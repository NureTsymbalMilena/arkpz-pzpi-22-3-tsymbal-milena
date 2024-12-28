from cryptography.hazmat.primitives.ciphers import Cipher, algorithms, modes
from cryptography.hazmat.backends import default_backend
import os
from dotenv import load_dotenv

"""
    Encrypts or decrypts data using XOR operation with the provided key.

    Args:
        data (bytes): Data to be encrypted or decrypted.
        key (bytes): Key used for XOR operation.

    Returns:
        bytes: The result of the XOR operation (encrypted or decrypted data).
"""
def xor_encrypt_decrypt(data, key):
    
    return bytes([data[i] ^ key[i % len(key)] for i in range(len(data))])

"""
    Encrypts a file using XOR encryption.

    Args:
        file_path (str): Path to the file to be encrypted.
        key (str or bytes): The encryption key to be used.
"""
def encrypt(file_path, key):
    
    try:
        key_bytes = key.encode() if isinstance(key, str) else key
        with open(file_path, 'rb') as file:
            data = file.read()
        encrypted_data = xor_encrypt_decrypt(data, key_bytes)
        with open(file_path, 'wb') as file:
            file.write(encrypted_data)
        print(f"File {file_path} encrypted successfully.")
    except Exception as e:
        print(f"Error encrypting file {file_path}: {e}")

"""
    Decrypts a file using XOR decryption.

    Args:
        file_path (str): Path to the file to be decrypted.
        key (str or bytes): The decryption key to be used.
"""
def decrypt(file_path, key):
    
    try:
        key_bytes = key.encode() if isinstance(key, str) else key
        with open(file_path, 'rb') as file:
            encrypted_data = file.read()
        decrypted_data = xor_encrypt_decrypt(encrypted_data, key_bytes)
        with open(file_path, 'wb') as file:
            file.write(decrypted_data)
        print(f"File {file_path} decrypted successfully.")
    except Exception as e:
        print(f"Error decrypting file {file_path}: {e}")


"""
    Finds the most recent backup file in the specified directory based on the given extension.

    Args:
        directory (str): The path to the directory where the backup files are stored.
        extension (str): The file extension to filter by (default is ".sql").

    Returns:
        str: The path to the most recent backup file, or None if no file is found.
"""
def find_latest_backup(directory, extension=".sql"):
    
    try:
        files = [f for f in os.listdir(directory) if f.endswith(extension)]
        if not files:
            return None
        files.sort(key=lambda x: os.path.getmtime(os.path.join(directory, x)), reverse=True)
        return os.path.join(directory, files[0])
    except Exception as e:
        print(f"Error while finding backup files: {e}")
        return None