#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightSource;

out vec4 finalColor;

// Set alpha from 0 to 20
int Alpha(float var1) {
    return int(floor(var1 * 20 + 0.5));
}

// Float distance
float Distance(vec2 var1, vec2 var2) {
    return sqrt(pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2));
}

// Direction -1 0 1
int Direction(int var1, int var2) {
    return (var1 > var2) ? 1 : ((var1 < var2) ? -1 : 0);
}

bool inShadow(int pixelAlpha, ivec2 lightLocation, ivec2 pixelLocation) {
    // Determine direction and slope
    ivec2 variance = ivec2(Direction(lightLocation.x, pixelLocation.x), Direction(lightLocation.y, pixelLocation.y));
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;

    // Variables for possible obstacles
    ivec2 currentPixelLocation = pixelLocation;
    vec2 accurateCurrentPixelLocation;
    int currentPixelHeight;

    // Variables to keep track of original direction light hits the obstacle
    bool isHorizontalIncrease = false;
    bool isHorizontalIncreaseNow = false;
    int currentPixelHeightNow = 0;

    // Bias
    float bias;

    while (currentPixelLocation.x != lightLocation.x || currentPixelLocation.y != lightLocation.y) {
        // Loop through exact pixels that respond to the slope
        if (abs(pixelLocation.x - lightLocation.x) > abs(pixelLocation.y - lightLocation.y)) {
            if (currentPixelLocation.y == lightLocation.y || floor((currentPixelLocation.x + variance.x) * a + b + 0.5) == currentPixelLocation.y) {
                currentPixelLocation.x += variance.x;
                isHorizontalIncrease = true;
            } else {
                currentPixelLocation.y += variance.y;
                isHorizontalIncrease = false;
            }
        } else {
            if (currentPixelLocation.x == lightLocation.x || floor((currentPixelLocation.y + variance.y - b) / a + 0.5) == currentPixelLocation.x) {
                currentPixelLocation.y += variance.y;
                isHorizontalIncrease = false;
            } else {
                currentPixelLocation.x += variance.x;
                isHorizontalIncrease = true;
            }
        }

        currentPixelHeight = Alpha(texelFetch(Texture, currentPixelLocation, 0).a);

        // If we hit an obstacle
        if (currentPixelHeight - pixelAlpha > 0) {
            if (currentPixelHeight > currentPixelHeightNow) {
                currentPixelHeightNow = currentPixelHeight;
                isHorizontalIncreaseNow = isHorizontalIncrease;
            }

            // Set up deppending on direction
            if (isHorizontalIncreaseNow) {
                accurateCurrentPixelLocation = vec2(currentPixelLocation.x - variance.x * 0.5, (currentPixelLocation.x - variance.x * 0.5) * a + b);
            } else {
                if (lightLocation.x == pixelLocation.x) {
                    accurateCurrentPixelLocation = vec2(currentPixelLocation.x, currentPixelLocation.y - variance.y * 0.5);
                } else {
                    accurateCurrentPixelLocation = vec2((currentPixelLocation.y - variance.y * 0.5 - b) / a, currentPixelLocation.y - variance.y * 0.5);
                }
            }
            
            // Calculate if in shadow with account to bias. If you change the bias people die
            bias = 0.05 * (currentPixelHeight - pixelAlpha - 19);
            if (Distance(lightLocation, accurateCurrentPixelLocation) - bias > Distance(pixelLocation, accurateCurrentPixelLocation)) {
                return true;
            }

        } else if (currentPixelHeight < currentPixelHeightNow) {
            currentPixelHeightNow = 0;
        }
    }
    return false;
}

void main() {
    vec4 pixelColor = texture(Texture, fragTexCoord);
    ivec2 lightLocation = ivec2(lightSource.xy);
    ivec2 pixelLocation = ivec2(gl_FragCoord.xy);
    int lightRange = 300;

    // Is pixel in shadow
    int pixelAlpha = Alpha(pixelColor.a);
    //Skip pixel if it's not on the level layer
    if (pixelAlpha == 0) {
        discard;
    }
    bool isPixelInShadow = inShadow(pixelAlpha, lightLocation, pixelLocation);

    float distance = Distance(lightLocation, pixelLocation);
    float noise = fract(sin(dot(vec2(lightLocation.x - pixelLocation.x, lightLocation.y - pixelLocation.y), vec2(12.9898, 78.233))) * 43758.5453) / 300.0;

    const vec3 COLD_COLOR = vec3(0.18, 0.20, 0.27);

    vec3 tint = (pixelAlpha / 80.0 + 0.3) * COLD_COLOR.xyz + noise;

    if (distance <= lightRange) {
        if (isPixelInShadow) {
            finalColor = vec4(tint, 1);
        } else {
            // float tint = pixelAlpha * (1.00 - distance / lightRange) / 150.00;
            
            // Add slight tint based on depth and distance
            // finalColor = vec4(pixelColor.x + tint + noise, pixelColor.y + (tint + noise) * 0.9, pixelColor.z + (tint + noise) * 0.8, 1);
            finalColor = vec4(pixelColor.xyz * (1.00 - pow(distance / lightRange, 2)) + tint * pow(distance / lightRange, 2), 1);
        }
    } else {
        // finalColor = vec4(pixelColor.xyz * 0.8, 1);
        finalColor = vec4(tint, 1);
    }
}