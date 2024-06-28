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

int Distance(ivec2 var1, ivec2 var2) {
    return int(floor(sqrt(pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2))));
}

float Shadow(int pixelAlpha, ivec2 lightLocation, ivec2 pixelLocation) {
    float strength = 0;
    float placeHolder = 0;
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;

    ivec2 location;
    ivec2 obstacleLocation;
    ivec2 variance = ivec2(-1, -1);

    bool horizontal = false;

    // Sets the search for obstacle location to always be increasing and determines if it's going vertically or horizontally depending on the difference.
    if (abs(lightLocation.x - pixelLocation.x) > abs(lightLocation.y - pixelLocation.y)) {
        horizontal = true;
        if (lightLocation.x > pixelLocation.x) {
            location = ivec2(pixelLocation.x, lightLocation.x);
        } else {
            location = ivec2(lightLocation.x, pixelLocation.x);
        }
    } else {
        if (lightLocation.y > pixelLocation.y) {
            location = ivec2(pixelLocation.y, lightLocation.y);
        } else {
            location = ivec2(lightLocation.y, pixelLocation.y);
        }
    }
    
    // Sets the variance depending on the direction of the light for edge cases.
    if (lightLocation.x > pixelLocation.x) {
        variance = ivec2(1, variance.y);
    }
    if (lightLocation.y > pixelLocation.y) {
        variance = ivec2(variance.x, 1);
    }

    // Checks if there is an obstacle between the light and the pixel.
    for (float i = location.x; i <= location.y; i += 1) {

        // Sets the obstacle location.
        if (horizontal) {
            obstacleLocation = ivec2(i, floor(i * a + b + 0.5));
        } else {
            if (pixelLocation.x == lightLocation.x) {
                obstacleLocation = ivec2(pixelLocation.x, i);
            } else {
                obstacleLocation = ivec2(floor((i - b) / a + 0.5), i);
            }
        }

        // Gets the alpha value of the obstacle and 2 surrounding pixels.
        int obstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);
        int horAlpha = Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + variance.x, obstacleLocation.y), 0).a);
        int verAlpha = Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + variance.y), 0).a);

        // If there is an obstacle, returns the strength of the light.
        if (obstacleAlpha > pixelAlpha || (horAlpha > pixelAlpha && verAlpha > pixelAlpha)) {

            // Determines if we are in an edge case.
            if (horAlpha > pixelAlpha && verAlpha > pixelAlpha) {
                obstacleAlpha = int((verAlpha + horAlpha) / 2);
            }
            strength = 10;
            break;
            // if (float(obstacleLocation.y - 0.5) < float(obstacleLocation.x + variance.x * 0.5) * a + b && float(obstacleLocation.x + variance.x * 0.5) * a + b < float(obstacleLocation.y + 0.5)) {
            //     strength = 10;
            //     break;
                // if (variance.x > 0 && obstacleLocation.x <= pixelLocation.x && pixelLocation.x <= obstacleLocation.x + variance.x * (obstacleAlpha - pixelAlpha)) {
                //     strength = 10;
                //     break;
                // } else if (variance.x < 0 && obstacleLocation.x + variance.x * (obstacleAlpha - pixelAlpha) <= pixelLocation.x && pixelLocation.x <= obstacleLocation.x) {
                //     strength = 10;
                //     break;
                // }
            // } 
            // else if (float(obstacleLocation.x - 0.5) < (float(obstacleLocation.y + variance.y * 0.5) - b) / a && (float(obstacleLocation.y + variance.y * 0.5) - b) / a < float(obstacleLocation.x + 0.5)) {
            //     strength = 10;
            //     break;
            // }
                // if (variance.y > 0 && obstacleLocation.y <= pixelLocation.y && pixelLocation.y <= obstacleLocation.y + variance.y * (obstacleAlpha - pixelAlpha)) {
                //     strength = 10;
                //     break;
                // } else if (variance.y < 0 && obstacleLocation.y + variance.y * (obstacleAlpha - pixelAlpha) <= pixelLocation.y && pixelLocation.y <= obstacleLocation.y) {
                //     strength = 10;
                //     break;
                // }
            // } else {
            //     strength = 0;
            //     break;
            // }
        }
    }

    return strength;
}

void main() {
    ivec2 lightLocation = ivec2(lightSource.xy);
    ivec2 pixelLocation = ivec2(gl_FragCoord.xy);
    vec4 pixelColor = texelFetch(Texture, pixelLocation, 0);

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