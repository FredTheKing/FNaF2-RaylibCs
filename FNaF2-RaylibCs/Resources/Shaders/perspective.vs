#version 330

in vec2 vertexPosition;  // The vertex position input
in vec2 vertexTexCoord;  // The texture coordinate input

uniform mat4 mvp;        // Model-View-Projection matrix
uniform float time;      // A time value to animate scrolling
uniform float intensity; // Perspective intensity

out vec2 fragTexCoord;   // Pass texture coordinates to the fragment shader

void main() {
    // Adjust position for perspective effect
    float perspective = 1.0 + intensity * vertexPosition.y;
    vec2 position = vertexPosition * vec2(perspective, 1.0);

    // Apply horizontal scrolling
    position.x += time;

    // Output the transformed position
    gl_Position = mvp * vec4(position, 0.0, 1.0);

    // Pass texture coordinates to the fragment shader
    fragTexCoord = vertexTexCoord;
}
