#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightPosition;
uniform vec2 lightStrength;
uniform vec2 resolution;

out vec4 finalColor;

void main() {
    //Get color from texture
    vec4 texColor = texture(ourTexture, fragTexCoord);

    //Skip pixel if it's not on the level layer
    if (texColor.a == 0) {
        discard;
    }

    //Set pixel coordinates from 1/1 to 320/180
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

    float shoa = int(lightStrength.x - sqrt((pow(lightPosition.x - pixelCoord.x, 2) + pow(lightPosition.y - pixelCoord.y, 2))));

    float a = 0;
    if (pixelCoord.x - lightPosition.x != 0) {
        a = (pixelCoord.y - lightPosition.y) / (pixelCoord.x - lightPosition.x);
    }
    float b = pixelCoord.y - a * pixelCoord.x;

    if (shoa > 0) {
        bool stop = false;
        for (int i = int(position.x) + 1; i < position.y; i++) {
            vec2 coord = vec2(0, 0);
            if (x) {
                coord = vec2(i / resolution.x, floor(i * a + b + 0.5) / resolution.y);
            } else {
                if (a == 0) {
                    coord = vec2(pixelCoord.x, i / resolution.y);
                } else {
                    coord = vec2(floor((i - b) / a + 0.5) / resolution.x, i / resolution.y);
                }
            }

            vec4 testColor = texture(ourTexture, coord);
            if (testColor.a > texColor.a) {
                stop = true;
                break;
            }
        }

        if (stop) {
            finalColor = vec4(texColor.x, texColor.y, texColor.z, 1);
        } else {
            finalColor = texColor + vec4(shoa / 255, shoa / 255, shoa / 255, 1);
        }
    } else {
        finalColor = vec4(texColor.x, texColor.y, texColor.z, 1);
    }
}