import json
import socket

"""
    Updates the connection string in the specified configuration file.

    Args:
        file_path (str): The path to the configuration file to be updated.
        db_name (str): The name of the database (default is "inRoomDb").
        host (str): The host address for the database connection (default is "localhost").
        port (int): The port number for the database connection (default is 5119).

    Returns:
        None: This function does not return any value. It prints a success message or error message depending on the outcome.
"""
def update_appsettings(file_path, db_name="InRoom", host="localhost", port=5119):
    try:
        with open(file_path, "r") as config_file:
            config_data = json.load(config_file)
        
        connection_string = f"Data Source={socket.gethostname()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Database={db_name}"

        if "ConnectionStrings" in config_data:
            config_data["ConnectionStrings"]["DefaultString"] = connection_string
        else:
            config_data["ConnectionStrings"] = {"DefaultString": connection_string}

        with open(file_path, "w") as config_file:
            json.dump(config_data, config_file, ensure_ascii=False, indent=4)
        
        print(f"Successfully updated the file: {file_path}")
    
    except FileNotFoundError:
        print(f"Error: The configuration file {file_path} does not exist.")
    except json.JSONDecodeError:
        print(f"Error: Failed to decode JSON in {file_path}.")
    except Exception as e:
        print(f"An unexpected error occurred while updating the file {file_path}: {e}")