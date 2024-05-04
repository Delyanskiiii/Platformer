#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

float Circle(vec2 var1, vec2 var2) {
    return floor(sqrt((pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2))) + 0.5);
}

float CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord) {
    float variance = 0;
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
                if (currentColor.a > alpha) {
                    variance += Circle(vec2(i, i * a + b), currentPixelCoord) - currentColor.a + alpha;
                } else if (leftColor.a > alpha && verColor.a > alpha) {
                    variance += Circle(vec2(i, i * a + b), currentPixelCoord) - leftColor.a + alpha;
                }
                if (variance >= 10) {
                    return variance;
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
                if (currentColor.a > alpha) {
                    variance += Circle(currentPixelCoord, vec2(i, i * a + b)) - currentColor.a + alpha;
                } else if (rightColor.a > alpha && verColor.a > alpha) {
                    variance += Circle(currentPixelCoord, vec2(i, i * a + b)) - rightColor.a + alpha;
                }
                if (variance >= 10) {
                    return variance;
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
                    if (currentColor.a > alpha) {
                        variance += Circle(currentPixelCoord, vec2((i - b) / a, i)) - currentColor.a + alpha;
                    } else if ((downColor.a > alpha && horColor.a > alpha)) {
                        variance += Circle(currentPixelCoord, vec2((i - b) / a, i)) - downColor.a + alpha;
                    }
                    if (variance >= 10) {
                        return variance;
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
                    if (currentColor.a > alpha) {
                        variance += Circle(currentPixelCoord, vec2((i - b) / a, i)) - currentColor.a + alpha;
                    } else if ((upColor.a > alpha && horColor.a > alpha)) {
                        variance += Circle(currentPixelCoord, vec2((i - b) / a, i)) - upColor.a + alpha;
                    }
                    if (variance >= 10) {
                        return variance;
                    }
                }
            }
        } else {
            if (lightPixelCoord.y > currentPixelCoord.y) {
                for (float i = currentPixelCoord.y + 1; i <= lightPixelCoord.y; i += 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        variance += Circle(currentPixelCoord, vec2(lightPixelCoord.x, i)) - currentColor.a + alpha;
                    }
                    if (variance >= 10) {
                        return variance;
                    }
                }
            } else {
                for (float i = currentPixelCoord.y - 1; i >= lightPixelCoord.y; i -= 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        variance += Circle(currentPixelCoord, vec2(lightPixelCoord.x, i)) - currentColor.a + alpha;
                    }
                    if (variance >= 10) {
                        return variance;
                    }
                }
            }
        }
    }
    return variance;
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
    vec2 lightCoord = vec2(lightSource.x + 0.5, lightSource.y + 0.5);
    
    float circle = lightProps.y - Circle(lightCoord, pos);
    float variance = CalculateColorVariance(texColor.a, lightCoord, pos);

    if (circle > 0 && variance < 10 && variance >= 0) {
        variance = (circle - variance) / 500;
        finalColor = vec4(texColor.x + variance, texColor.y + variance, texColor.z + variance, 1);
    } else {
        finalColor = vec4(texColor.xyz, 1);
    }
}