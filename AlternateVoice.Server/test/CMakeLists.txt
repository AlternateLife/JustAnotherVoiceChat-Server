# Setup test project
project(AlternateVoiceTest LANGUAGES CXX)

# Add executable
add_executable(AlternateVoiceTest test.cpp)

# Link library to test
target_link_libraries(AlternateVoiceTest AlternateVoice.Server)
