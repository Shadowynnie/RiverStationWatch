# River Station Watch

River Station Watch is a web application designed to monitor and display water level records for various stations. The application features real-time search, sorting, flood level alerts, and more, providing a comprehensive tool for water station data management.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## Features

- **Real-time Data:** Monitor water levels in real-time.
- **Flood Alerts:** Get notified when water levels exceed predefined flood thresholds.
- **Search Functionality:** Search through records quickly and efficiently.
- **Sorting:** Sort records by different columns such as station, timestamp, and value.
- **Auto Reload:** Enable or disable auto-reloading of the page.
- **Delete Records:** Remove records with a simple click.
- **Load More Records:** Load additional records dynamically as needed.

## Installation

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js](https://nodejs.org/) (for front-end dependencies)

### Steps

1. **Clone the repository:**
    ```bash
    git clone https://github.com/yourusername/RiverStationWatch.git
    cd RiverStationWatch
    ```

2. **Install dependencies:**
    ```bash
    dotnet restore
    ```

3. **Build the application:**
    ```bash
    dotnet build
    ```

4. **Run the application:**
    ```bash
    dotnet run
    ```

5. **Navigate to the application in your web browser:**
    ```bash
    http://localhost:5000
    ```

## Usage

### Flood Alerts
When water levels exceed the predefined flood levels at any station, a SweetAlert2 alert will be displayed with details of the affected stations.

### Sorting Records
Click on the column headers to sort the records. The sorting order will be saved in the browser's local storage.

### Auto Reload
Enable or disable auto-reloading of the page by using the checkbox at the top of the records table.

### Delete Records
Use the delete button next to any record to remove it from the list.

### Load More Records
Click the "Load More Records" button to fetch additional records dynamically.

## Technologies Used

- **ASP.NET Core 6.0**: For building the server-side application.
- **Razor Pages**: For creating the dynamic web pages.
- **Entity Framework Core**: For database access.
- **Bootstrap**: For responsive UI design.
- **jQuery**: For DOM manipulation and AJAX requests.
- **SweetAlert2**: For beautiful alerts and notifications.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License



## Acknowledgements

- [SweetAlert2](https://sweetalert2.github.io/) for the alert system.
- [Bootstrap](https://getbootstrap.com/) for the responsive design framework.
- [jQuery](https://jquery.com/) for simplifying JavaScript interactions.

## Contact

For any questions or feedback, please contact [your email](mailto:your-email@example.com).

