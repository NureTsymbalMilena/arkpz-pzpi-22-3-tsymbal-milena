import os
from installer import setup_sqlserver, setup_dotnet, setup_chocolatey, setup_task
from db_init import initialize_database
from config_updater import update_appsettings
from encrypter import encrypt, decrypt
from dotenv import load_dotenv
import getpass

"""
    Finds the most recent backup file in the specified directory.

    Args:
        directory (str): Path to the directory containing backup files.
        extension (str): File extension to filter by (default is ".bak").

    Returns:
        str: Path to the most recent backup file, or None if no file is found.
"""
def find_latest_backup(directory, extension=".bak"):
    try:
        files = [f for f in os.listdir(directory) if f.endswith(extension)]
        if not files:
            return None
        files.sort(key=lambda x: os.path.getmtime(os.path.join(directory, x)), reverse=True)
        return os.path.join(directory, files[0])
    except Exception as e:
        print(f"Error while finding backup files: {e}")
        return None

"""
    Main function to deploy the server.

    This function ensures that prerequisites like software dependencies and configurations are set up.
    It checks for the encryption key, installs necessary dependencies, restores the database, and updates configuration files.

    Returns:
        None
"""
def main():
    print("Checking prerequisites...")

    load_dotenv()  
    encryption_key = os.getenv("ENCRYPTION_KEY")

    if not encryption_key:
        print("Encryption key not found in .env file. Exiting.")
        return

    encryption_key = encryption_key.encode()

    setup_chocolatey()
    setup_sqlserver()
    setup_dotnet()
    setup_task()
    
    db_name = "InRoom"

    script_dir = os.path.dirname(os.path.abspath(__file__))
    backup_directory = os.path.join(script_dir, "..\\InRoom.DAL\\Migrations") 

    backup_directory = os.path.normpath(backup_directory)
    backup_file = find_latest_backup(backup_directory)
    if not backup_file:
        print(f"No backup files found in {backup_directory}")
        return
    print(f"Using backup file: {backup_file}")

    user_key = getpass.getpass("Enter the encryption key to decrypt the migration file: ").encode()

    if user_key == encryption_key:
        print("Key is correct. Decrypting the file...")
        decrypt(backup_file, encryption_key)
    else:
        print("Invalid key. Exiting.")
        return

    if not initialize_database(backup_file):
        print("Failed to configure the database. Exiting.")
        return

    encrypt(backup_file, encryption_key)

    appsettings_path = os.path.join(script_dir, "../InRoom.API/appsettings.Development.json")
    print(appsettings_path)
    if os.path.exists(appsettings_path):
        update_appsettings(appsettings_path)
    else:
        print(f"Configuration file {appsettings_path} not found. Skipping configuration update.")

    print("Server deployment completed successfully!")

if __name__ == "__main__":
    main()
