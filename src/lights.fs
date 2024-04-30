#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

float roundToSecondDecimal(float value) {
    return floor(value * 100) / 100;
}

bool CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord) {
    if (roundToSecondDecimal(abs(lightPixelCoord.x - currentPixelCoord.x) * 16) > roundToSecondDecimal(abs(lightPixelCoord.y - currentPixelCoord.y) * 9)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        if (lightPixelCoord.x > currentPixelCoord.x) {
            for (float i = currentPixelCoord.x + (1 / resolution.x); i < lightPixelCoord.x; i += (1 / resolution.x)) {
                vec4 currentColor = texture(ourTexture, vec2(i, i * a + b));
                vec4 leftColor = texture(ourTexture, vec2(i - 1 / resolution.x, i * a + b));
                vec4 upColor = texture(ourTexture, vec2(i, i * a + b + 1 / resolution.y));
                vec4 downColor = texture(ourTexture, vec2(i, i * a + b - 1 / resolution.y));
                if (currentColor.a > alpha || (leftColor.a > alpha && (upColor.a > alpha || downColor.a > alpha))) {
                    return false;
                }
            }
        } else {
            for (float i = currentPixelCoord.x - (1 / resolution.x); i > lightPixelCoord.x; i -= (1 / resolution.x)) {
                vec4 currentColor = texture(ourTexture, vec2(i, i * a + b));
                vec4 rightColor = texture(ourTexture, vec2(i + 1 / resolution.x, i * a + b));
                vec4 upColor = texture(ourTexture, vec2(i, i * a + b + 1 / resolution.y));
                vec4 downColor = texture(ourTexture, vec2(i, i * a + b - 1 / resolution.y));
                if (currentColor.a > alpha || (rightColor.a > alpha && (upColor.a > alpha || downColor.a > alpha))) {
                    return false;
                }
            }
        }
    } else {
        float a = 0;
        if (currentPixelCoord.x != lightPixelCoord.x) {
            a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        }
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        if (lightPixelCoord.y >= currentPixelCoord.y) {
            for (float i = currentPixelCoord.y + (1 / resolution.y); i < lightPixelCoord.y; i += (1 / resolution.y)) {
                vec4 currentColor = texture(ourTexture, vec2((i - b) / a, i));
                vec4 downColor = texture(ourTexture, vec2((i - b) / a, i - 1 / resolution.y));
                vec4 leftColor = texture(ourTexture, vec2((i - b) / a - 1 / resolution.x, i));
                vec4 rightColor = texture(ourTexture, vec2((i - b) / a + 1 / resolution.x, i));
                if (currentColor.a > alpha || (downColor.a > alpha && (leftColor.a > alpha || rightColor.a > alpha))) {
                    return false;
                }
            }
        } else {
            for (float i = currentPixelCoord.y - (1 / resolution.y); i > lightPixelCoord.y; i -= (1 / resolution.y)) {
                vec4 currentColor = texture(ourTexture, vec2((i - b) / a, i));
                vec4 upColor = texture(ourTexture, vec2((i - b) / a, i + 1 / resolution.y));
                vec4 leftColor = texture(ourTexture, vec2((i - b) / a - 1 / resolution.x, i));
                vec4 rightColor = texture(ourTexture, vec2((i - b) / a + 1 / resolution.x, i));
                if (currentColor.a > alpha || (upColor.a > alpha && (leftColor.a > alpha || rightColor.a > alpha))) {
                    return false;
                }
            }
        }
        // return false;
    }
    return true;
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
    
    // if (lightProps.y  - floor(sqrt((pow(lightCoord.x - fragTexCoord.x, 2) + pow(lightCoord.y - fragTexCoord.y, 2))) + 0.5) > 0) {
    //     colorVariance = CalculateColorVariance(texColor.a, lightSource, pixelCoord, colorVariance);
    // }
    if (CalculateColorVariance(texColor.a, lightCoord, fragTexCoord)) {
        finalColor = vec4(texColor.x + 0.5, texColor.y + 0.5, texColor.z + 0.5, 1);
    } else {
        finalColor = vec4(texColor.xyz, 1);
    }
}