#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightProps;
uniform vec2 lightSource;

out vec4 finalColor;

int Alpha(float var1) {
    return int(floor(var1 * 20 + 0.5));
}

float Distance(ivec2 var1, ivec2 var2) {
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
    ivec2 variance = ivec2(Direction(pixelLocation.x, lightLocation.x), Direction(pixelLocation.y, lightLocation.y));
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;
    bool direction;
    ivec2 currentPixelLocation = lightLocation;

    while (currentPixelLocation.x != pixelLocation.x || currentPixelLocation.y != pixelLocation.y) {
        if (currentPixelLocation.y == pixelLocation.y) {
            currentPixelLocation.x += variance.x;
            direction = true;
        } else if (currentPixelLocation.x == pixelLocation.x) {
            currentPixelLocation.y += variance.y;
            direction = false;
        } else if (floor((currentPixelLocation.x + variance.x) * a + b + 0.5) == currentPixelLocation.y) {
            currentPixelLocation.x += variance.x;
            direction = true;
        } else if (floor((currentPixelLocation.x + variance.x) * a + b + 0.5) != currentPixelLocation.y) {
            currentPixelLocation.y += variance.y;
            direction = false;
        }

        if (Alpha(texelFetch(Texture, currentPixelLocation, 0).a) > pixelAlpha) {

            float angle = asin((currentPixelLocation.y - lightLocation.y) / sqrt(pow(currentPixelLocation.x - lightLocation.x, 2) + pow(currentPixelLocation.y - lightLocation.y, 2)));

            if (direction) {
                if (abs(angle) < acos(-1.0) / 4) {
                    if (abs(pixelLocation.x - currentPixelLocation.x) <= Alpha(texelFetch(Texture, currentPixelLocation, 0).a) - pixelAlpha) {
                        return 10;
                    }
                } else {
                    if (abs(pixelLocation.y - currentPixelLocation.y) <= Alpha(texelFetch(Texture, currentPixelLocation, 0).a) - pixelAlpha) {
                        return 10;
                    }
                }
            } else {
                if (abs(angle) < acos(-1.0) / 4) {
                    if (abs(pixelLocation.x - currentPixelLocation.x) <= Alpha(texelFetch(Texture, currentPixelLocation, 0).a) - pixelAlpha) {
                        return 10;
                    }
                } else {
                    if (abs(pixelLocation.y - currentPixelLocation.y) <= Alpha(texelFetch(Texture, currentPixelLocation, 0).a) - pixelAlpha) {
                        return 10;
                    }
                }
            }
        }
    }
    return 0;
}

void main() {
    vec4 pixelColor = texture(Texture, fragTexCoord);
    ivec2 lightLocation = ivec2(lightSource.xy);
    ivec2 pixelLocation = ivec2(gl_FragCoord.xy);

    //Skip pixel if it's not on the level layer
    // if (pixelColor.a == 0) {
    //     discard;
    // }
    
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