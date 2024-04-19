#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightPosition;
uniform vec2 lightStrength;

out vec4 finalColor;

void main() {
    vec4 texColor = texture(ourTexture, fragTexCoord);
    // if (gl_FragCoord <= lightPosition + lightStrength && gl_FragCoord >= lightPosition - lightStrength) {

    // }
    if (fragTexCoord.x > lightPosition.x) {
        finalColor = vec4(1,1,1,1);
    } else {
        finalColor = texColor;
    }


    // vec2 newCoord = vec2(fragTexCoord.x + 0.1, fragTexCoord.y - 100);

    // finalColor = vec4(texColor.x, texColor.y, texColor.z, texColor.alpha);
}