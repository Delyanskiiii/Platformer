#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

bool CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord) {
    if (abs(lightPixelCoord.x - currentPixelCoord.x) > abs(lightPixelCoord.y - currentPixelCoord.y)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        if (lightPixelCoord.x > currentPixelCoord.x) {
            for (float i = currentPixelCoord.x + 1; i <= lightPixelCoord.x; i += 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 leftColor = texture(ourTexture, vec2((i - 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 verColor;
                if (b < a + b) {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                } else {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                }
                if (currentColor.a > alpha || (leftColor.a > alpha && verColor.a > alpha)) {
                    return false;
                }
            }
        } else {
            for (float i = currentPixelCoord.x - 1; i >= lightPixelCoord.x; i -= 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 rightColor = texture(ourTexture, vec2((i + 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 verColor;
                if (b < a + b) {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                } else {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                }
                if (currentColor.a > alpha || (rightColor.a > alpha && verColor.a > alpha)) {
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

        if (a != 0) {
            if (lightPixelCoord.y > currentPixelCoord.y) {
                for (float i = currentPixelCoord.y + 1; i <= lightPixelCoord.y; i += 1) {
                    vec4 currentColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, i / resolution.y));
                    vec4 downColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, (i - 1) / resolution.y));
                    vec4 horColor;
                    if (-b / a < (1 - b) / a) {
                        horColor = texture(ourTexture, vec2(((i - b) / a - 1) / resolution.x, i / resolution.y));
                    } else {
                        horColor = texture(ourTexture, vec2(((i - b) / a + 1) / resolution.x, i / resolution.y));
                    }
                    if (currentColor.a > alpha || (downColor.a > alpha && horColor.a > alpha)) {
                        return false;
                    }
                }
            } else {
                for (float i = currentPixelCoord.y - 1; i >= lightPixelCoord.y; i -= 1) {
                    vec4 currentColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, i / resolution.y));
                    vec4 upColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, (i + 1) / resolution.y));
                    vec4 horColor;
                    if (-b / a < (1 - b) / a) {
                        horColor = texture(ourTexture, vec2(((i - b) / a + 1) / resolution.x, i / resolution.y));
                    } else {
                        horColor = texture(ourTexture, vec2(((i - b) / a - 1) / resolution.x, i / resolution.y));
                    }
                    if (currentColor.a > alpha || (upColor.a > alpha && horColor.a > alpha)) {
                        return false;
                    }
                }
            }
        } else {
            if (lightPixelCoord.y > currentPixelCoord.y) {
                for (float i = currentPixelCoord.y + 1; i <= lightPixelCoord.y; i += 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        return false;
                    }
                }
            } else {
                for (float i = currentPixelCoord.y - 1; i >= lightPixelCoord.y; i -= 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        return false;
                    }
                }
            }
        }
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
    vec2 pos = vec2(gl_FragCoord.x, resolution.y - gl_FragCoord.y);
    
    // vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));
    // vec2 lightCoord = vec2((lightSource.x) / resolution.x, (lightSource.y) / resolution.y);
    vec2 lightCoord = vec2(lightSource.x + 0.5, lightSource.y + 0.5);
    
    float variance = lightProps.y - floor(sqrt((pow(lightCoord.x - pos.x, 2) + pow(lightCoord.y - pos.y, 2))) + 0.5);
    if (variance > 0 && CalculateColorVariance(texColor.a, lightCoord, pos)) {
        finalColor = vec4(texColor.x + variance / 255, texColor.y + variance / 255, texColor.z + variance / 255, 1);
    } else {
        finalColor = vec4(texColor.xyz, 1);
    }
}