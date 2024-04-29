#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

vec3 CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord, vec3 colorVariance) {
    // if (abs(lightPixelCoord.x - currentPixelCoord.x) < 1 / resolution.x && abs(lightPixelCoord.y - currentPixelCoord.y) < 1 / resolution.y) {
    //     colorVariance = vec3(1, 1, 1);
    // } else 
    if (floor(abs(lightPixelCoord.x * resolution.x - currentPixelCoord.x * resolution.x) + 0.5) >= floor(abs(lightPixelCoord.y * resolution.y - currentPixelCoord.y * resolution.y) + 0.5)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        bool stop = true;
        if (lightPixelCoord.x > currentPixelCoord.x) {
            for (float i = lightPixelCoord.x - 1 / resolution.x; i > currentPixelCoord.x + 1 / resolution.x; i -= 1 / resolution.x) {
                vec2 coord = vec2(i, i * a + b);
                vec4 testColor = texture(ourTexture, coord);
                if (testColor.a > alpha) {
                    stop = false;
                    break;
                }
            }
        } else {
            for (float i = lightPixelCoord.x + 1 / resolution.x; i < currentPixelCoord.x - 1 / resolution.x; i += 1 / resolution.x) {
                vec2 coord = vec2(i, i * a + b);
                vec4 testColor = texture(ourTexture, coord);
                if (testColor.a > alpha) {
                    stop = true;
                    break;
                }
            }
        }

        if (!stop) {
            colorVariance = vec3(0, 0, 0);
        } else {
            colorVariance = vec3(0.5, 0.5, 0.5);
        }
    } else {
        colorVariance = vec3(0, 0, 0);
    }
    return colorVariance;
}

void main() {
    //Get color from texture
    vec4 texColor = texture(ourTexture, fragTexCoord);

    //Skip pixel if it's not on the level layer
    if (texColor.a == 0) {
        // discard;
    }

    //Set pixel coordinates in correct resolution 320/180
    // vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));
    vec2 lightCoord = vec2(lightSource.x / resolution.x, lightSource.y / resolution.y);
    vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));
    vec3 colorVariance = vec3(0, 0, 0);
    
    // if (lightProps.y  - floor(sqrt((pow(lightCoord.x - fragTexCoord.x, 2) + pow(lightCoord.y - fragTexCoord.y, 2))) + 0.5) > 0) {
    //     colorVariance = CalculateColorVariance(texColor.a, lightSource, pixelCoord, colorVariance);
    // }
    colorVariance = CalculateColorVariance(texColor.a, lightSource, pixelCoord, colorVariance);

    finalColor = vec4(texColor.x + colorVariance.x, texColor.y + colorVariance.y, texColor.z + colorVariance.z, 1);
}