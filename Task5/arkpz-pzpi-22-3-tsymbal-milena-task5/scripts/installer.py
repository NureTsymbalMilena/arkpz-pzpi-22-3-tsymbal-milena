import os
import subprocess
import shutil
import socket

"""
    Executes a command in the shell and returns True if successful.

    Args:
        command (str): The shell command to be executed.

    Returns:
        bool: True if the command executes successfully, False otherwise.
"""
def run_command(command):
    try:
        result = subprocess.run(command, shell=True, check=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
        return True
    except FileNotFoundError:
        print(f"FileNotFoundError: Command not found - {command}")
        return False
    except subprocess.CalledProcessError as e:
        print(f"Command failed: {e}")
        print(f"stderr: {e.stderr}")
        return False

"""
    Adds a directory to the system PATH environment variable.

    Args:
        directory (str): The directory to be added to the PATH.
"""
def add_to_path(directory):
    if directory not in os.environ["PATH"]:
        os.environ["PATH"] += os.pathsep + directory
        print(f"Added {directory} to PATH.")
    else:
        print(f"{directory} is already in PATH.")

"""
    Checks if Chocolatey is installed, and installs it if missing.

    Returns:
        bool: True if Chocolatey is installed or successfully installed, False otherwise.
"""
def setup_chocolatey():
    print("\nChecking for Chocolatey...")
    if not shutil.which("choco"):
        print("Chocolatey is not installed. Installing...")
        install_command = (
            'powershell -NoProfile -ExecutionPolicy Bypass -Command "' 
            'Set-ExecutionPolicy Bypass -Scope Process -Force; ' 
            'iex ((New-Object System.Net.WebClient).DownloadString(\'https://community.chocolatey.org/install.ps1\'))"'
        )
        print("Running installation command...")
        if run_command(install_command):
            print("Chocolatey installed successfully.")
            add_to_path("C:\\ProgramData\\chocolatey\\bin")
        else:
            print("Failed to install Chocolatey. Please install it manually.")
            return False
    else:
        print("Chocolatey is already installed.")
    return True

"""
    Checks if SQL Server is installed, installs it if missing, and configures a default user.

    Returns:
        None
"""
def setup_sqlserver():
    print("\nChecking for SQL Server...")

    if not shutil.which("sqlcmd"):
        print("SQL Server is not installed. Installing...")
        if run_command(["choco", "install", "mssql-server", "-y"]):
            print("SQL Server installed successfully.")
        else:
            print("Failed to install SQL Server. Please install it manually.")
            return
    else:
        print("SQL Server is already installed.")

    default_username = socket.gethostname()
    default_password = "your_password_here"

    os.environ['SQLCMDPASSWORD'] = default_password

    default_username_escaped = f"[{default_username}]"

    create_user_command = f"""
    IF NOT EXISTS (SELECT * FROM sys.syslogins WHERE name = '{default_username_escaped}')
    BEGIN
        CREATE LOGIN {default_username_escaped} WITH PASSWORD = '{default_password}';
    END
    """

    print(f"\nChecking if user '{default_username}' exists...")
    if not run_command(["sqlcmd", "-S", "localhost", "-E", "-Q", create_user_command]):
        print(f"Failed to create user '{default_username}'. Ensure you have superuser access.")
    else:
        print(f"User '{default_username}' exists or has been created successfully.")

    print(f"Granting CREATE DATABASE privilege to user '{default_username}'...")
    grant_privilege_command = f"ALTER LOGIN {default_username_escaped} ENABLE;"

    if not run_command(["sqlcmd", "-S", "localhost", "-E", "-Q", grant_privilege_command]):
        print(f"Failed to grant CREATE DATABASE privilege to user '{default_username}'. Ensure you have superuser access.")
    else:
        print(f"CREATE DATABASE privilege granted to user '{default_username}'.")

"""
    Checks if .NET SDK is installed, installs it if missing, and ensures it is in PATH.

    Returns:
        None
"""
def setup_dotnet():
    print("\nChecking for .NET SDK...")

    if not run_command(["dotnet", "--version"]):
        print(".NET SDK is not installed. Installing...")

        if run_command(["choco", "install", "dotnet-8.0-sdk", "--version=8.0.404", "-y"]):
            print(".NET SDK installed successfully.")
        else:
            print("Failed to install .NET SDK. Please install it manually.")
            return
    else:
        print(".NET SDK is already installed.")

    dotnet_path = "C:\\Program Files\\dotnet"
    if os.path.isdir(dotnet_path):
        add_to_path(dotnet_path)
    else:
        print(f"Directory {dotnet_path} does not exist. Ensure .NET SDK is installed correctly.")

"""
    Checks if Task command-line utility is installed, installs it if missing, and adds it to PATH.

    Returns:
        None
"""
def setup_task():
    if not run_command(["task", "--version"]):
        print("Task command not found, installing...")
        subprocess.run(["choco", "install", "go-task", "-y"], check=True)
        taskfile_path = os.path.join(os.environ.get('TASKPROFILE', ''), 'taskfile', 'bin')
            
        if taskfile_path and taskfile_path not in os.environ['PATH']:
            os.environ['PATH'] += os.pathsep + taskfile_path
            print(f"Added {taskfile_path} to PATH.")
            print("Task installed successfully.")
        else:
            print("\nFailed to install Task using Chocolatey.")
    else:
        print("\nTask is already installed.")