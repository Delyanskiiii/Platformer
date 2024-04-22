#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightPosition;
uniform vec2 lightStrength;
uniform int resolution;

out vec4 finalColor;

void main() {
    vec4 texColor = texture(ourTexture, fragTexCoord);
    texColor = vec4(texColor.x - 50/255, texColor.y - 50/255, texColor.z - 50/255, texColor.a);
    vec2 pixelCoord = vec2(floor(fragTexCoord.x * 320 * resolution + 0.5), floor(fragTexCoord.y * 180 * resolution + 0.5));

    // if (abs(lightPosition.x - pixelCoord.x) > abs(lightPosition.y - pixelCoord.y)) {
    //     vec2 position = vec2(pixelCoord.x, lightPosition.x);
    // } else {
    //     vec2 position = vec2(pixelCoord.y, lightPosition.y);
    // }

    float shoa = lightStrength.x - (pow(lightPosition.x - pixelCoord.x, 2) + pow(lightPosition.y - pixelCoord.y, 2));
    if (shoa > 0) {
        finalColor = vec4(texColor.x + shoa, texColor.y + shoa, texColor.z + shoa, texColor.a);
    } else {
        finalColor = texColor;
    }
    // finalColor = texColor;
    // for (int i = -1; i <= 1; i++) {
    //     for (int j = -1; j <= 1; j++) {
    //         pixelCoord.x 
    //     }
    // }

    // if (gl_FragCoord <= lightPosition + lightStrength && gl_FragCoord >= lightPosition - lightStrength) {

    // }


    // if (lightPosition.x > 2000) {
    //     finalColor = vec4(1,1,1,1);
    // } else {
    //     finalColor = texColor;
    // }

    // finalColor = vec4(fragTexCoord.x, 0, 0, 1);
    // vec2 newCoord = vec2(fragTexCoord.x + 0.1, fragTexCoord.y - 100);

    // finalColor = vec4(texColor.x, texColor.y, texColor.z, texColor.alpha);
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