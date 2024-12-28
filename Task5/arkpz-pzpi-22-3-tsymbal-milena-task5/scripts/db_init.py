import subprocess
import os
import sys

"""
    Initializes the database by creating it and restoring it from a backup file.

    Args:
        backup_path (str): The path to the backup file used for restoring the database.
        database_name (str): The name of the database to be created and restored (default is "inRoomDb").

    Returns:
        bool: True if the database setup was successful, None otherwise.
"""
def initialize_database(backup_path, database_name="InRoom"):
    print("Starting database setup...")

    try:
        check_db_query = f"sqlcmd -S localhost -E -Q \"SELECT COUNT(*) FROM sys.databases WHERE name = '{database_name}'\""
        check_db_response = subprocess.run(check_db_query, capture_output=True, text=True, check=True, shell=True)
        
        if "1" in check_db_response.stdout.strip():
            print(f"Database '{database_name}' already exists.")
        else:
            create_db_query = f"sqlcmd -S localhost -E -Q \"CREATE DATABASE {database_name}\""
            subprocess.run(create_db_query, capture_output=True, text=True, check=True, shell=True)
            print(f"Database '{database_name}' successfully created.")
    except subprocess.CalledProcessError as e:
        print(f"Error checking or creating database '{database_name}': {e.stderr}")
        return

    try:
        restore_db_query = f"sqlcmd -S localhost -E -Q \"RESTORE DATABASE {database_name} FROM DISK = '{backup_path}'\""
        restore_db_response = subprocess.run(restore_db_query, capture_output=True, text=True, check=True, shell=True)
        print("Database restoration from backup completed successfully.")
    except subprocess.CalledProcessError as e:
        print(f"Error restoring database from {backup_path}.")
        print(f"stdout: {e.stdout}")
        print(f"stderr: {e.stderr}")
        return

    print("Database setup successfully completed.")
    return True
