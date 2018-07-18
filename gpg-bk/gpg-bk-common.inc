################################################################################
# gpg-bk - Common functions and variables.
# Copyright (c) 2008-2018 Open Communications Security
#
# Licensed under BSD 3-Clause License
################################################################################

# Location of the backup gpg keys
GPG_HOME=$MY_HOME/gpg

# Verifies if the backup directory is initialized.
is_initialized() {
	test -f "$GPG_HOME/trustdb.gpg"
}

# Extracts the email of the backup key.
get_key_mail() {
	gpg --homedir "$GPG_HOME" -K | grep '\[ultimate\]' | sed -s 's/.*<//;s/>.*//'
}

