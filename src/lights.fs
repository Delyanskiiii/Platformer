#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left, pixel_center_integer) in vec4 gl_FragCoord;

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
    int strength = 0;
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;
    
    ivec2 variance = ivec2(Direction(pixelLocation.x, lightLocation.x), Direction(pixelLocation.y, lightLocation.y));
    ivec2 obstacleLocation;
    int alphaDifference;
    int side = 0;

    if (pixelLocation.x + 0.5 * variance.x == lightLocation.x - 0.5 * variance.x) {
        for (float i = lightLocation.y + 0.5 * variance.y; i != pixelLocation.y - 0.5 * variance.y; i += variance.y) {
            obstacleLocation = ivec2(lightLocation.x, int(i + 0.5 * variance.y));

            int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

            if (possibleObstacleAlpha > pixelAlpha) {
                alphaDifference = possibleObstacleAlpha - pixelAlpha;
                side = 1;
                break;
            }
        }
    } else {
        float lasty = lightLocation.y - 0.5 * variance.y;
        for (float i = lightLocation.x + 0.5 * variance.x; i != pixelLocation.x - 0.5 * variance.x; i += variance.x) {
            float y = floor(i * a + b + 0.5) - 0.5 * variance.y;
            while (y != lasty) {
                obstacleLocation = ivec2(int(floor((lasty + variance.y - b) / a + 0.5)), int(lasty + variance.y + 0.5 * variance.y));

                int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

                if (possibleObstacleAlpha > pixelAlpha) {
                    alphaDifference = possibleObstacleAlpha - pixelAlpha;

                    // if ((lasty + variance.y - b) / a == i) {
                    //     side = 2;
                    // } else {
                    //     side = 1;
                    // }
                    side = 1;
                    break;
                }
                lasty += variance.y;
            }

            if (side != 0) {
                break;
            }
            
            obstacleLocation = ivec2(int(i + 0.5 * variance.x), int(floor(i * a + b + 0.5)));

            int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

            if (possibleObstacleAlpha > pixelAlpha) {
                alphaDifference = possibleObstacleAlpha - pixelAlpha;
                // if (i * a + b == lasty + variance.y) {
                //     side = 2;
                // } else {
                //     side = -1;
                // }
                side = -1;
                break;
            }
        }
    }

    if (side == 2) {
        if (abs(pixelLocation.y - obstacleLocation.y) <= alphaDifference && abs(pixelLocation.x - obstacleLocation.x) <= alphaDifference) {
            strength = 10;
        }
        strength = 100;
    } else if (side == 1) {
        if (abs(pixelLocation.y - obstacleLocation.y) <= alphaDifference) {
            strength = 10;
        }
    } else if (side == -1) {
        if (abs(pixelLocation.x - obstacleLocation.x) <= alphaDifference) {
            strength = 10;
        }
    }
    return strength;
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