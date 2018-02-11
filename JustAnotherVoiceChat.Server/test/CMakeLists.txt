# Setup test project
project(JustAnotherVoiceChatTest LANGUAGES CXX)

include_directories(../include)
include_directories(../thirdparty/enet/include)

file(GLOB SOURCES "./*.cpp")

# Add executable
add_executable(JustAnotherVoiceChatTest ${SOURCES})

# Link library to test
target_link_libraries(JustAnotherVoiceChatTest JustAnotherVoiceChat.Server)
