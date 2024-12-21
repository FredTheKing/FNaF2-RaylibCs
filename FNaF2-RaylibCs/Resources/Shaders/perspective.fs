#version 330

in vec2 fragTexCoord;    // Interpolated texture coordinates from the vertex shader
uniform sampler2D texture0; // The game texture
out vec4 fragColor;      // Final pixel color output

void main() {
  fragColor = texture(texture0, fragTexCoord);
}