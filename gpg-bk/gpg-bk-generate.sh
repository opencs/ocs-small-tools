#!/bin/bash
################################################################################
# gpg-bk-generate.sh - Script used to generate the gpg-bk key.
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
if is_initialized; then
	echo "The directory $GPG_HOME already exists."
	exit 1
else
	mkdir "$GPG_HOME"
fi

# Generate
if gpg --batch --homedir "$GPG_HOME" --full-generate-key; then
	echo "Key generated."
else
	echo "Unable to generate the key."
	exit
fi

# Backup copies
pushd "$MY_HOME" > /dev/null
mkdir bk
BK_FILE=bk/$GPG_HOME_NAME.tar.gz
tar -czf $BK_FILE $GPG_HOME_NAME
for i in `seq 1 256`; do
	dst=$BK_FILE.$(printf "%03d" $i)
	cp $BK_FILE $dst
done
popd > /dev/null

