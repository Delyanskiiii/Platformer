#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

vec3 CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord, vec3 colorVariance) {
    if (abs(currentPixelCoord.x - lightPixelCoord.x) <= 1 && abs(currentPixelCoord.y - lightPixelCoord.y) <= 1) {
        colorVariance = vec3(1, 1, 1);
    } else if (abs(lightPixelCoord.x - currentPixelCoord.x) > abs(lightPixelCoord.y - currentPixelCoord.y)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        int variance = 1;
        if (lightPixelCoord.x > currentPixelCoord.x) {
            variance = -1;
        }

        bool stop = false;
        for (float i = lightPixelCoord.x + variance; i != currentPixelCoord.x - variance; i+= variance) {
            vec2 coord = vec2(i / resolution.x, floor(i * a + b + 0.5) / resolution.y);
            vec4 testColor = texture(ourTexture, coord);
            if (testColor.a > alpha) {
                stop = true;
                break;
            }
        }

        if (stop) {
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
        discard;
    }

    //Set pixel coordinates in correct resolution 320/180
    vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));
    vec3 colorVariance = vec3(0, 0, 0);
    
    if (lightProps.y - floor(sqrt((pow(lightSource.x - pixelCoord.x, 2) + pow(lightSource.y - pixelCoord.y, 2))) + 0.5) > 0) {
        colorVariance = CalculateColorVariance(texColor.a, lightSource, pixelCoord, colorVariance);
    }
    
    finalColor = vec4(texColor.x + colorVariance.x, texColor.y + colorVariance.y, texColor.z + colorVariance.z, 1);
}