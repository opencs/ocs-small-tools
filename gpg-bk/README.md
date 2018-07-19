# gpg-bk

## Description

The **gpg-bk** is a simple set of scripts used to create gpg keys that can be used to
safely encrypt backup files for transport and/or storage on an untrusted location.

The idea is to create the keys inside a distinct GPG home directory that can be
safely stored offline (e.g.: optical media) and used only when the backup files are
necessary.

## Dependencies

gpg-bk depends on the following tools:

* GnuPG 2.2 or later;
* GNU bash 4.4 of later:
* GNU sed 4.4 or later;
* GNU grep 3.1 or later;
* GNU tar 1.29 or later;

Those scripts are so simple that they may run or previous version of those tools but
this scenario was not tested.

## gpg-bk Tools

* bk-gpg<span></span>.sh: A shell script that executes gpg pointing to the backup home directory;
* gpg-bk-export<span></span>.sh: Exports the backup public key;
* gpg-bk-generate<span></span>.sh: Generates the backup key pair;

## How to use it

### Generate the key

Just go to the script directory and run **gpg-bk-generate<span></span>.sh**. Do not forget to
create a large key in order to ensure a long term security for the files.

### Export the public key

Just go to the script directory and run **gpg-bk-export<span></span>.sh**.

### Using the backup keys

Just go to the script directory and run **gpg-bk<span></span>.sh**. It is a simple
shell over the existing GnuPG program but points to the backup key home.

## How it works

This scripts creates a new GnuPG keyring with the backup private key and store in in the **gpg**
subdirectory parallel to the scripts home. It also creates multiple compressed backup copies
of the **gpg** subdirectory and add them to the **bk** subdirectory.

The purpose of those copies is to ensure a minimum redundancy when the contents of this directory
is stored inside an optical media because at least one copy is expected to be be readable in case
of minor damage in the disc.

## Licensing

**gpg-bk** is licensed under BSD 3-Clause License.

## Throubleshooting

### 'No such file or directory' while executing 'gpg-bk-generate<span></span>.sh'

During the execution of **gpg-bk-generate<span></span>.sh**, just before the key generation,you receive the message:

```
gpg: agent_genkey failed: No such file or directory
Key generation failed: No such file or directory
```

This problem is caused by a previous execution of the **gpg-agent** for another home directory pointing. Solve this by killing the **gpg-agent** with:

```
$ pkill pg-agent
```
