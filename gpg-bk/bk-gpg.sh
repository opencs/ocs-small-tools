#!/bin/bash
################################################################################
# gpg-bk.sh - Script used to execute the gpg-bk
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
#	exit 1
fi

# Execute the GPG command with the new home!
gpg --homedir "$GPG_HOME" $*

