# Data Warehouse for Wastedge

The Data Warehouse for Wastedge application is a collection of Windows applications to implement data warehouse services for Wastedge.

The Data Warehouse for Wastedge application works with the Wastedge REST API. For more information on the Wastedge
API, see the documentation in the [Wastedge REST API](https://github.com/wastedge/weapi) wiki.

## Downloading

The setup for the Data Warehouse for Wastedge application can be downloaded from the
[releases page on GitHub](https://github.com/wastedge/data-warehouse/releases).

## Projects

The Data Warehouse for Wastedge application contains a few different projects:

* `Wastedge.DataWarehouse`: The core data warehouse library that implements the data warehousing logic. This library is used by the CLI application and Windows service, and can be used by your own project to implement data warehousing functionality;
* `Wastedge.DataWarehouse.Cli`: Provides a CLI interface to the data warehouse logic. This can be used for testing purposes or to run synchronization from a script;
* `Wastedge.DataWarehouse.Manager`: A Windows application to manage the Wastedge Data Warehouse. This application allows you to setup connections to one or more databases. This connection will in the background setup a Windows Service to perform the synchronization;
* `Wastedge.DataWarehouse.Service`: The Windows Service installed by the management application;
* `Wastedge.DataWarehouse.Service.Cli`: A developers tool to aid in developing and debugging the Windows service.

## Bugs

Please report bugs on the [GitHub Issues](https://github.com/wastedge/data-warehouse/issues) page.

## License

The Wastedge API for Java library is licensed under the [Apache 2.0 license](https://github.com/wastedge/data-warehouse/blob/master/LICENSE).
