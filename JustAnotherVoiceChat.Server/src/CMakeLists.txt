# Setup library project
add_definitions(-DJUSTANOTHERVOICECHAT_EXPORTS)

# Search required libraries
find_package(Threads)

# Add files to library
file(GLOB SOURCES "./*.cpp")

add_library(JustAnotherVoiceChat.Server SHARED ${SOURCES})

# Link required libraries
target_link_libraries(JustAnotherVoiceChat.Server enet)

target_link_libraries(JustAnotherVoiceChat.Server ${CMAKE_THREAD_LIBS_INIT})

if(WIN32)
  target_link_libraries(JustAnotherVoiceChat.Server ws2_32)
  target_link_libraries(JustAnotherVoiceChat.Server winmm)
endif(WIN32)

# Setup install directories
install(TARGETS JustAnotherVoiceChat.Server ARCHIVE DESTINATION lib LIBRARY DESTINATION lib RUNTIME DESTINATION bin)
install(DIRECTORY ${CMAKE_SOURCE_DIR}/include/ DESTINATION include/JustAnotherVoiceChat)
