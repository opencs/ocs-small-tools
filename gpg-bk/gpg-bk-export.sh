#!/bin/bash
################################################################################
# gpg-bk-export.sh - Script used to export the gpg-bk key.
# Copyright (c) 2008-2018 Open Communications Security
#
# Licensed under BSD 3-Clause License
################################################################################

# Determine MY_HOME
MY_HOME=$(dirname "$0")
pushd "$MY_HOME" > /dev/null
MY_HOME=$(pwd)
popd > /dev/null

# Load the configuration
. "$MY_HOME/gpg-bk-common.inc"

# Check if the file is initalized
if ! is_initialized; then
	echo "Not initialized."
	exit 1
fi

KEY_NAME=$(get_key_mail)
if gpg --homedir "$GPG_HOME" --export -r $KEY_NAME -a > "$MY_HOME/$KEY_NAME.asc"; then
	echo "Key exported."
else
	echo "Unable to export the key."
	exit 1
fi

