# Nursing Home Management Application

## Overview
The Nursing Home Management Application is a desktop software solution designed to manage medication for elderly residents in a nursing home. It streamlines communication and processes among healthcare professionals, ensuring efficient medication management and resident care.

## Features
- **Doctor Interface:** Systematic registration of medications for residents.
- **Nurse Interface:** A medication administration schedule, allowing nurses to track and manage medication intakes.
- **Admin Interface:** Management of staff and residents, including oversight of medication orders and scheduling shifts for nurses.

## Technology Stack
- **Platform:** Desktop Application
- **Development Environment:** Visual Studio 2019
- **Programming Language:** C# with Windows Forms

## Architecture
The application is built using a three-layer architecture model, known as the DNA model. This structure divides the application into three distinct layers, each handling different aspects of the software:

1. **Data Access Layer (DAL):** 
   - Manages the connection between the application and the database, ensuring efficient data retrieval and storage.

2. **Business Logic Layer (BLL):**
   - Contains classes that represent the data model of the project and implements various logical functions related to the application's core functionalities.

3. **Graphic User Interface (GUI):**
   - Provides the user interface for the application using Windows Forms, allowing users to interact with the application effectively.

## Implementation Details
Each layer consists of multiple classes and/or forms, with well-defined variables and objects specific to each class. This organization facilitates easier code writing and maintenance, promoting a clear separation of concerns.

While there is interconnectivity among the classes and forms across the different layers, it is achieved with a focus on data security and functional integrity.

## Conclusion
The Nursing Home Management Application aims to enhance the efficiency and quality of medication management in nursing homes, ultimately improving the standard of care for elderly residents.
