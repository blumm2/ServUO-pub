EXENAME=ServUO
CONFIG=release
BUILD_DIR=${CURDIR}
EXE=$(BUILD_DIR)/$(EXENAME).dll

# Solution platform matching the machine (x64 on PCs, ARM64 on e.g. a Raspberry Pi)
PLATFORM ?= $(if $(filter aarch64 arm64,$(shell uname -m)),ARM64,x64)

.PHONY: all build run clean release debug

all: build run

release: CONFIG = release
release: run

debug: CONFIG = debug
debug: run

build:
	@echo "Compile $(EXENAME) for $(shell uname -s) $(PLATFORM) ($(CONFIG))"
	@dotnet build -c $(CONFIG) -p:Platform=$(PLATFORM)
	@echo "Done!"

run: build
	@if [ -f "$(EXE)" ]; then \
		clear; \
		echo "Running $(EXENAME) in $(CONFIG) mode..."; \
		dotnet $(EXE) -$(CONFIG); \
	else \
		echo "Executable not found. Did the build succeed?"; \
		exit 1; \
	fi

clean:
	@dotnet clean -p:Platform=$(PLATFORM)
	@echo "Cleaned build files."
