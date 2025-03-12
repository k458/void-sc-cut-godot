#!/bin/sh
echo -ne '\033c\033]0;void-sc-cut\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/void-sc-cut(linux).x86_64" "$@"
