#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;
uniform vec2 active;

out vec4 finalColor;

float Circle(vec2 var1, vec2 var2) {
    if (abs(var1.x - var2.x) > abs(var1.y - var2.y)) {
        return abs(var1.y - var2.y);
    } else {
        return abs(var1.x - var2.x);
    }
    // return floor(sqrt((pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2))) + 0.5);
}

float Shadow(float circle, float alpha) {
    if (alpha < 0.01) {
        return 100;
    }
    if (alpha >= 0.25) {
        return 25;
    } else if (circle > alpha * 100) {
        return 0;
    } else {
        return (alpha * 100 - circle);
    }
}

float ShadowStrength(float rootPixelAlpha, vec2 lightPixelCoord, vec2 rootPixelCoord) {
    float strength = 0;
    float a = (rootPixelCoord.y - lightPixelCoord.y) / (rootPixelCoord.x - lightPixelCoord.x);
    float b = rootPixelCoord.y - a * rootPixelCoord.x;
    vec2 currentPixelCoord;

    if (abs(lightPixelCoord.x - rootPixelCoord.x) > abs(lightPixelCoord.y - rootPixelCoord.y)) {
        
    }


    return strength;
}

float CalculateShadowStrength(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord) {
    float variance = 0;
    float var = 0;
    if (abs(lightPixelCoord.x - currentPixelCoord.x) > abs(lightPixelCoord.y - currentPixelCoord.y)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        if (lightPixelCoord.x > currentPixelCoord.x) {
            for (float i = currentPixelCoord.x + 1; i <= lightPixelCoord.x; i += 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 leftColor = texture(ourTexture, vec2((i - 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 verColor;
                if (a > 0) {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                } else {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                }
                if (currentColor.a > alpha) {
                    var = Shadow(Circle(vec2(i, i * a + b), currentPixelCoord), currentColor.a - alpha);
                    if (var > variance) {
                        variance = var;
                    }
                }
                if (leftColor.a > alpha && verColor.a > alpha) {
                    var = Shadow(Circle(vec2(i, i * a + b), currentPixelCoord), leftColor.a - alpha);
                    if (var > variance) {
                        variance = var;
                    }
                }
            }
        } else {
            for (float i = currentPixelCoord.x - 1; i >= lightPixelCoord.x; i -= 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 rightColor = texture(ourTexture, vec2((i + 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 verColor;
                if (a > 0) {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                } else {
                    verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                }
                if (currentColor.a > alpha) {
                    var = Shadow(Circle(vec2(i, i * a + b), currentPixelCoord), currentColor.a - alpha);
                    if (var > variance) {
                        variance = var;
                    }
                }
                if (rightColor.a > alpha && verColor.a > alpha) {
                    var = Shadow(Circle(vec2(i, i * a + b), currentPixelCoord), rightColor.a - alpha);
                    if (var > variance) {
                        variance = var;
                    }
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
                    // -b / a < (1 - b) / a
                    if (a > 0) {
                        horColor = texture(ourTexture, vec2(((i - b) / a - 1) / resolution.x, i / resolution.y));
                    } else {
                        horColor = texture(ourTexture, vec2(((i - b) / a + 1) / resolution.x, i / resolution.y));
                    }
                    if (currentColor.a > alpha) {
                        var = Shadow(Circle(vec2((i - b) / a, i), currentPixelCoord), currentColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
                    } 
                    if ((downColor.a > alpha && horColor.a > alpha)) {
                        var = Shadow(Circle(vec2((i - b) / a, i), currentPixelCoord), downColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
                    }
                }
            } else {
                for (float i = currentPixelCoord.y - 1; i >= lightPixelCoord.y; i -= 1) {
                    vec4 currentColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, i / resolution.y));
                    vec4 upColor = texture(ourTexture, vec2(((i - b) / a) / resolution.x, (i + 1) / resolution.y));
                    vec4 horColor;
                    if (a > 0) {
                        horColor = texture(ourTexture, vec2(((i - b) / a + 1) / resolution.x, i / resolution.y));
                    } else {
                        horColor = texture(ourTexture, vec2(((i - b) / a - 1) / resolution.x, i / resolution.y));
                    }
                    if (currentColor.a > alpha) {
                        var = Shadow(Circle(vec2((i - b) / a, i), currentPixelCoord), currentColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
                    } 
                    if ((upColor.a > alpha && horColor.a > alpha)) {
                        var = Shadow(Circle(vec2((i - b) / a, i), currentPixelCoord), upColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
                    }
                }
            }
        } else {
            if (lightPixelCoord.y > currentPixelCoord.y) {
                for (float i = currentPixelCoord.y + 1; i <= lightPixelCoord.y; i += 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        var = Shadow(Circle(vec2(lightPixelCoord.x, i), currentPixelCoord), currentColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
                    }
                }
            } else {
                for (float i = currentPixelCoord.y - 1; i >= lightPixelCoord.y; i -= 1) {
                    vec4 currentColor = texture(ourTexture, vec2(lightPixelCoord.x / resolution.x, i / resolution.y));
                    if (currentColor.a > alpha) {
                        var = Shadow(Circle(vec2(lightPixelCoord.x, i), currentPixelCoord), currentColor.a - alpha);
                        if (var > variance) {
                            variance = var;
                        }
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
    vec4 color = vec4(texColor.xyz, texColor.a);
    if (active.x == 1) {
        //Skip pixel if it's not on the level layer
        if (color.a == 0) {
            discard;
        }

        //Set pixel coordinates in correct resolution 320/180
        vec2 pos = vec2(gl_FragCoord.x, resolution.y - gl_FragCoord.y);
        vec2 lightCoord = vec2(lightSource.x + 0.5, lightSource.y + 0.5);
        
        float distance = Circle(lightCoord, pos);
        //strength is from 0 to 25
        float strength = CalculateShadowStrength(color.a, lightCoord, pos);

        if (strength > 0) {
            finalColor = vec4(color.xyz - strength / 150, 1);
        } else {
            // finalColor = vec4(color.xyz + (float(int(color.a * 100) % 10) / 10 + (0.35 - distance / 960)) / 50, 1);
            finalColor = vec4(color.xyz + (1 - distance / 320) / 5, 1);
        }
    } else {
        finalColor = vec4(color.xyz, 1);
    }
}