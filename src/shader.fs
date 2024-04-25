#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightPosition;
uniform vec2 lightStrength;
uniform vec2 resolution;

out vec4 finalColor;

void main() {
    vec4 texColor = texture(ourTexture, fragTexCoord);
    // texColor = vec4(texColor.x - 50/255, texColor.y - 50/255, texColor.z - 50/255, texColor.a);
    vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));

    vec2 position = vec2(0, 0);
    bool x = true;

    if (abs(lightPosition.x - pixelCoord.x) > abs(lightPosition.y - pixelCoord.y)) {
        if (pixelCoord.x < lightPosition.x) {
            position = vec2(pixelCoord.x, lightPosition.x);
        } else {
            position = vec2(lightPosition.x, pixelCoord.x);
        }
        x = true;
    } else {
        if (pixelCoord.y < lightPosition.y) {
            position = vec2(pixelCoord.y, lightPosition.y);
        } else {
            position = vec2(lightPosition.y, pixelCoord.y);
        }
        x = false;
    }

    float shoa = lightStrength.x - sqrt((pow(lightPosition.x - pixelCoord.x, 2) + pow(lightPosition.y - pixelCoord.y, 2)));

    float a = (pixelCoord.y - lightPosition.y) / (pixelCoord.x - lightPosition.x);
    float b = pixelCoord.y - a * pixelCoord.x;

    if (shoa > 0 && texColor.a > 0) {
        bool stop = false;
        for (int i = int(position.x) + 1; i < position.y; i++) {
            vec2 coord = vec2(0, 0);
            if (x) {
                coord = vec2(i / resolution.x, (i * a + b) / resolution.y);
            } else {
                coord = vec2((i - b) / a / resolution.x, i / resolution.y);
            }

            vec4 testColor = texture(ourTexture, coord);
            if (testColor.a > texColor.a) {
                stop = true;
                break;
            }
        }

        if (stop) {
            finalColor = texColor;
        } else {
            finalColor = texColor + vec4(shoa / 255, shoa / 255, shoa / 255, 1);
        }
    } else {
        finalColor = texColor;
    }
    // finalColor = texColor;
    // for (int i = -1; i <= 1; i++) {
    //     for (int j = -1; j <= 1; j++) {
    //         pixelCoord.x 
    //     }
    // }
}

// void convertToInteger(in float value, out int outputValue) {
//     outputValue = floor(value + 0.5);
// }

// float convertToInteger(float value) {
//     return floor(value + 0.5);
// }

// float convertToFloat(int value, bool width) {
//     if (width) {
//         return value / (resolution * 320);
//     } else {
//         return value / (resolution * 180);
//     }
// }