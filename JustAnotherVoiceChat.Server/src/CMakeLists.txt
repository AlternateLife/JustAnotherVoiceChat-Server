# Setup library project
project(JustAnotherVoiceChat.Server VERSION 1.0.0 LANGUAGES CXX)

add_definitions(-DJUSTANOTHERVOICECHAT_EXPORTS)

# Add files to library
include_directories(../include)
include_directories(../thirdparty/enet/include)

file(GLOB SOURCES "./*.cpp")

add_library(JustAnotherVoiceChat.Server SHARED ${SOURCES})

# Link required libraries
target_link_libraries(JustAnotherVoiceChat.Server enet)

if(WIN32)
target_link_libraries(JustAnotherVoiceChat.Server ws2_32)
target_link_libraries(JustAnotherVoiceChat.Server winmm)
endif(WIN32)

# Setup install directories
install(TARGETS JustAnotherVoiceChat.Server ARCHIVE DESTINATION lib LIBRARY DESTINATION lib RUNTIME DESTINATION bin)
install(DIRECTORY ${CMAKE_SOURCE_DIR}/include/ DESTINATION include/JustAnotherVoiceChat)
