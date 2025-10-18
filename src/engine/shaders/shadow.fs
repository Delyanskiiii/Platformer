#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightProps;
uniform vec2 lightSource;

out vec4 finalColor;

// alpha goes from 0 to 20
int Alpha(float var1) {
    return int(floor(var1 * 20 + 0.5));
}

float Distance(vec2 var1, vec2 var2) {
    return sqrt(pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2));
}

int Direction(int var1, int var2) {
    if (var1 > var2) {
        return 1;
    } else if (var1 < var2) {
        return -1;
    } else {
        return 0;
    }
}

float Shadow(int pixelAlpha, ivec2 lightLocation, ivec2 pixelLocation) {
    ivec2 variance = ivec2(Direction(lightLocation.x, pixelLocation.x), Direction(lightLocation.y, pixelLocation.y));
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;
    ivec2 currentPixelLocation = pixelLocation;
    vec2 accuratePixelLocation, accurateCurrentPixelLocation, accurateLightLocation;
    // int height = 0;
    int currentPixelHeight = 0, lightPixelHeight = Alpha(texelFetch(Texture, lightLocation, 0).a);
    bool shoa = false;
    bool SHOA = false;
    int SHOAHeight = 0;

    while (currentPixelLocation.x != lightLocation.x || currentPixelLocation.y != lightLocation.y) {

        if (abs(pixelLocation.x - lightLocation.x) > abs(pixelLocation.y - lightLocation.y)) {
            if (currentPixelLocation.y == lightLocation.y || floor((currentPixelLocation.x + variance.x) * a + b + 0.5) == currentPixelLocation.y) {
                currentPixelLocation.x += variance.x;
                shoa = true;
            } else {
                currentPixelLocation.y += variance.y;
                shoa = false;
            }
        } else {
            if (currentPixelLocation.x == lightLocation.x || floor((currentPixelLocation.y + variance.y - b) / a + 0.5) == currentPixelLocation.x) {
                currentPixelLocation.y += variance.y;
                shoa = false;
            } else {
                currentPixelLocation.x += variance.x;
                shoa = true;
            }
        }


        currentPixelHeight = Alpha(texelFetch(Texture, currentPixelLocation, 0).a);

        if (currentPixelHeight - pixelAlpha > 0 && currentPixelHeight - lightPixelHeight > 0) {

            if (currentPixelHeight > SHOAHeight) {
                SHOAHeight = currentPixelHeight;
                SHOA = shoa;
            }

            if (SHOA) {
                accurateCurrentPixelLocation = vec2(currentPixelLocation.x - variance.x * 0.5, (currentPixelLocation.x - variance.x * 0.5) * a + b);
            } else {
                if (lightLocation.x == pixelLocation.x) {
                    accurateCurrentPixelLocation = vec2(currentPixelLocation.x, currentPixelLocation.y - variance.y * 0.5);
                } else {
                    accurateCurrentPixelLocation = vec2((currentPixelLocation.y - variance.y * 0.5 - b) / a, currentPixelLocation.y - variance.y * 0.5);
                }
            }
            
            if (Distance(pixelLocation, accurateCurrentPixelLocation) * 20 / Distance(lightLocation, pixelLocation) <= currentPixelHeight - pixelAlpha) {
                if (SHOA) {
                    return 10;
                } else {
                    return 10;
                }
            }
        } else if (currentPixelHeight < SHOAHeight) {
            SHOAHeight = 0;
        }
    }
    return 0;
}

void main() {
    vec4 pixelColor = texture(Texture, fragTexCoord);
    ivec2 lightLocation = ivec2(lightSource.xy);
    ivec2 pixelLocation = ivec2(gl_FragCoord.xy);

    //Skip pixel if it's not on the level layer
    if (pixelColor.a == 0) {
        discard;
    }
    
    // distance is from 0 to 367
    // int distance = Distance(lightLocation, pixelLocation);

    float strength = Shadow(Alpha(pixelColor.a), lightLocation, pixelLocation);

    if (strength > 0) {
        finalColor = vec4(pixelColor.xyz - strength / 100, 1);
    } else {
        finalColor = vec4(pixelColor.xyz, 1);
        // finalColor = vec4(pixelColor.xyz + (0.1 - float(distance / 3670)), 1);
    }
}