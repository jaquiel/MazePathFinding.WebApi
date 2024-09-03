
# Maze Pathfinding API

## Objective

The Maze Pathfinding API is a RESTful service that allows users to submit maze configurations, find paths through the mazes, and retrieve information about previously saved mazes. The service is designed to work with mazes up to 20x20 in size and supports movement in the up, down, left, and right directions.

## Maze Format

Mazes are represented as a two-dimensional array of characters, where:

- `S`: Start point (exactly one)
- `G`: Goal point (exactly one)
- `_`: Empty space
- `X`: Wall

### Example Maze

```plaintext
S _ _ _ _
X X X X _
_ _ _ X _
_ X _ X _
_ X _ _ G
````

## Constraints

1.  **Maximum maze size**: 20 rows x 20 columns
2.  **Allowed movements**: Up, down, left, right (diagonal movements are not allowed)

## Requirements

-   The API has two main endpoints:
    
    1.  **POST /api/mazes**: Submit a new maze configuration, returning a possible solution or an error if a solution is not found.
    2.  **GET /api/mazes**: Retrieve a list of all previously submitted mazes and their solutions (if any).
-   **Data storage**: All maze data is stored in memory. A database is not used.
    
-   **Performance**: The application must return responses in less than 10 seconds.
    

## Usage

### Submit a New Maze

**Endpoint**: `POST /api/mazes`

#### Request Example

```json
{
    "grid": [
        ["S", "_", "_", "_", "_"],
        ["X", "X", "X", "X", "_"],
        ["_", "_", "_", "X", "_"],
        ["_", "X", "_", "X", "_"],
        ["_", "X", "_", "_", "G"]
    ]
} 
```

#### Response Examples (Success)

```json
{
  "solution": "S  -> [0,0] -> [0,1] -> [0,2] -> [0,3] -> [0,4] -> [1,4] -> [2,4] -> [3,4] -> [4,4] -> G"
}
```

```json
{ "solution" = "No solution found for the maze." }
```

#### Response Examples (Error)

```json
{ "error" = "Maze must contain one start point (S) and one goal point (G)." }
```

```json
{ "error" = "Invalid maze grid." }
```


```json
{
  "error": "Maze exceeds maximum size of 20 rows x 20 columns."
}
``` 

```json
{ "error" = "Maze must have the same number of columns in each row." }
```

### List Submitted Mazes

**Endpoint**: `GET /api/mazes`

#### Response Example

```json
[
  {
    "grid": [
      [ "S", "_", "_", "_", "_" ],
      [ "X", "X", "X", "X", "_" ],
      [ "_", "_", "_", "X", "_" ],
      [ "_", "X", "_", "X", "_" ],
      [ "_", "X", "_", "_", "G" ]
    ],
    "start": {
      "item1": 0,
      "item2": 0
    },
    "goal": {
      "item1": 4,
      "item2": 4
    },
    "solution": [
      [ 0, 0 ],
      [ 0, 1 ],
      [ 0, 2 ],
      [ 0, 3 ],
      [ 0, 4 ],
      [ 1, 4 ],
      [ 2, 4 ],
      [ 3, 4 ],
      [ 4, 4 ]
    ]
  }
]
```

## How to Run

1.  **Clone the repository**:
    
```
$ git clone https://github.com/jaquiel/MazePathFinding.WebApi.git
$ cd MazePathFinding.WebApi` 
```
    
2.  **Restore dependencies**:
     
```
$ dotnet restore
``` 
    
3.  **Build and run the application**:
       
 ```
 $ dotnet run
 ```
    
4.  **Access the API**:
    
    -   The API will be available at:
	    -  	`http://localhost:5000/api/mazes`
	    -  	`https://localhost:5001/api/mazes`

## Testing

The API includes unit tests that can be run using the command:

```
$ dotnet test