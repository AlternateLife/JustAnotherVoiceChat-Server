# Setup library project
project(AlternateVoice.Server VERSION 1.0.0 LANGUAGES CXX)

add_definitions(-DALTERNATEVOICE_EXPORTS)

# Add files to library
include_directories(../include)
file(GLOB SOURCES "./*.cpp")

add_library(AlternateVoice.Server SHARED ${SOURCES})

# Link required libraries
find_package(PkgConfig REQUIRED)
pkg_check_modules(ENET REQUIRED libenet)

target_link_libraries(AlternateVoice.Server ${ENET_LIBRARIES})
target_include_directories(AlternateVoice.Server PUBLIC ${ENET_INCLUDE_DIRS})
target_compile_options(AlternateVoice.Server PUBLIC ${ENET_CFLAGS_OTHER})

# Setup install directories
install(TARGETS AlternateVoice.Server ARCHIVE DESTINATION lib LIBRARY DESTINATION lib RUNTIME DESTINATION bin)
install(DIRECTORY ${CMAKE_SOURCE_DIR}/include/ DESTINATION include/AlternateVoice)
