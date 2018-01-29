# Setup test project
project(AlternateVoiceTest LANGUAGES CXX)

include_directories(../include)
include_directories(../thirdparty/enet/include)

file(GLOB SOURCES "./*.cpp")

# Add executable
add_executable(AlternateVoiceTest ${SOURCES})

# Link library to test
target_link_libraries(AlternateVoiceTest AlternateVoice.Server)
