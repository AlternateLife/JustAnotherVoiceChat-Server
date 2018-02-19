# Setup test project
file(GLOB SOURCES "./*.cpp")

# Add executable
add_executable(JustAnotherVoiceChatTest ${SOURCES})

# Link library to test
target_link_libraries(JustAnotherVoiceChatTest JustAnotherVoiceChat.Server)

add_dependencies(JustAnotherVoiceChatTest JustAnotherVoiceChat.Server)
