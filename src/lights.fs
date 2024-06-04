#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left, pixel_center_integer) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

float Distance(vec2 var1, vec2 var2) {
    if (abs(var1.x - var2.x) > abs(var1.y - var2.y)) {
        return abs(var1.x - var2.x);
    } else {
        return abs(var1.y - var2.y);
    }  
}

int Shadow(vec2 var1, vec2 var2, int var3, int var4, int var5, int var6, int alpha) {
    int diff1 = int(abs(var1.x - var2.x));
    int diff2 = int(abs(var1.y - var2.y));

    if (diff1 > alpha && diff2 > alpha) {
        return 0;
    } else if (diff1 <= alpha && diff2 <= alpha) {
        return alpha;
    } else {
        if (diff1 > alpha) {
            if (var3 > 0 || var4 > 0) {
                return alpha;
            } else {
                return 0;
            }
        } else {
            if (var5 > 0 || var6 > 0) {
                return alpha;
            } else {
                return 0;
            }
        }
    }
}

float Shadowdi(bool circle, float alpha) {
    if (circle) {
        return alpha;
    } else {
        return 0;
    }
    // if (alpha < 0.01) {
    //     //return 100;
    // }
    // if (alpha >= 0.55) {
    //     return 25;
    // } else if (circle > alpha * 100) {
    //     return 0;
    // } else {
    //     return (alpha * 100 - circle);
    // }
}

float ShadowStrength(int rootPixelAlpha, vec2 lightPixelCoord, vec2 rootPixelCoord) {
    float strength = 0;
    float var = 0;
    float a = (rootPixelCoord.y - lightPixelCoord.y) / (rootPixelCoord.x - lightPixelCoord.x);
    float b = rootPixelCoord.y - a * rootPixelCoord.x;
    bool vertical = false;

    if (rootPixelCoord.x == lightPixelCoord.x) {
        vertical = true;
    }

    vec2 pos;
    vec2 variance = vec2(-1, -1);
    bool horizontal = false;

    if (abs(lightPixelCoord.x - rootPixelCoord.x) > abs(lightPixelCoord.y - rootPixelCoord.y)) {
        horizontal = true;
        if (lightPixelCoord.x > rootPixelCoord.x) {
            pos = vec2(rootPixelCoord.x, lightPixelCoord.x);
        } else {
            pos = vec2(lightPixelCoord.x, rootPixelCoord.x);
        }
    } else {
        if (lightPixelCoord.y > rootPixelCoord.y) {
            pos = vec2(rootPixelCoord.y, lightPixelCoord.y);
        } else {
            pos = vec2(lightPixelCoord.y, rootPixelCoord.y);
        }
    }
    
    if (lightPixelCoord.x > rootPixelCoord.x) {
        variance = vec2(1, variance.y);
    }

    if (lightPixelCoord.y > rootPixelCoord.y) {
        variance = vec2(variance.x, 1);
    }

    ivec2 currentCoord;

    for (float i = pos.x; i <= pos.y; i += 1) {
        if (horizontal) {
            currentCoord = ivec2(i, floor(i * a + b + 0.5));
        } else {
            if (vertical) {
                currentCoord = ivec2(rootPixelCoord.x, i);
            } else {
                currentCoord = ivec2(floor((i - b) / a + 0.5), i);
            }
        }

        int currentAlpha = int(texelFetch(Texture, currentCoord, 0).a * 100);
        int horAlpha = int(texelFetch(Texture, ivec2(currentCoord.x + variance.x, currentCoord.y), 0).a * 100);
        int betahorAlpha = int(texelFetch(Texture, ivec2(currentCoord.x - variance.x, currentCoord.y), 0).a * 100);
        int verAlpha = int(texelFetch(Texture, ivec2(currentCoord.x, currentCoord.y + variance.y), 0).a * 100);
        int betaverAlpha = int(texelFetch(Texture, ivec2(currentCoord.x, currentCoord.y - variance.y), 0).a * 100);

        if (currentAlpha > rootPixelAlpha) {
            var = Shadow(currentCoord, rootPixelCoord, horAlpha - rootPixelAlpha, betahorAlpha - rootPixelAlpha, verAlpha - rootPixelAlpha, betaverAlpha - rootPixelAlpha, currentAlpha - rootPixelAlpha);
            if (var > strength) {
                strength = var;
            }
        } else if (horAlpha > rootPixelAlpha && verAlpha > rootPixelAlpha) {
            var = Shadow(currentCoord, rootPixelCoord, horAlpha - rootPixelAlpha, betahorAlpha - rootPixelAlpha, verAlpha - rootPixelAlpha, betaverAlpha - rootPixelAlpha, int((verAlpha + horAlpha) / 2) - rootPixelAlpha);
            if (var > strength) {
                strength = var;
            }
        }
    }

    return strength;
}

void main() {
    vec4 texColor = texture(Texture, fragTexCoord);

    //Skip pixel if it's not on the level layer
    // if (texColor.a == 0) {
    //     discard;
    // }
    
    float distance = Distance(lightSource.xy, gl_FragCoord.xy);
    float strength = ShadowStrength(int(texColor.a * 100), lightSource.xy, gl_FragCoord.xy);

    if (strength > 0) {
        finalColor = vec4(texColor.xyz - strength / 150, 1);
    } else {
        finalColor = vec4(texColor.xyz + (1 - distance / 320) / 5, 1);
    }
}