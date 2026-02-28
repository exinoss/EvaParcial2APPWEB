<img width="1354" height="626" alt="image" src="https://github.com/user-attachments/assets/00e88766-e580-4d78-b825-3837a5b44553" />
<img width="1351" height="628" alt="image" src="https://github.com/user-attachments/assets/e79a8f06-96b1-4fed-acab-ce03cd07c818" />
<img width="1340" height="615" alt="image" src="https://github.com/user-attachments/assets/689f0506-ed24-4d65-a676-3dc05efbe139" />
<img width="1343" height="628" alt="image" src="https://github.com/user-attachments/assets/dfc167a1-1b2c-488a-87a1-84c936e7734a" />
<img width="1357" height="620" alt="image" src="https://github.com/user-attachments/assets/7aa694e9-f1c8-40da-85e0-522d29b8ad19" />
<img width="1357" height="624" alt="image" src="https://github.com/user-attachments/assets/c907146a-f5a8-493d-b79d-57e02c3d7653" />
<img width="537" height="405" alt="image" src="https://github.com/user-attachments/assets/8646ab61-e4ef-4db4-af2a-c73768da2a45" />



## Variables de Entorno

Crear un archivo `.env` en la raíz del proyecto backend (`/backend/.env`) con el siguiente contenido:

```
DB_CONNECTION_STRING=Server=localhost;Database=bdbiblioteca;Trusted_Connection=True;TrustServerCertificate=True;
```

---

## Endpoints de la API

**Base URL:** `https://localhost:7059/api` _(el puerto puede variar)_

---

### Libros

#### Obtener todos los libros

```
GET /api/Libros
```

#### Obtener un libro por ID

```
GET /api/Libros/{id}
```

#### Crear un libro

```
POST /api/Libros
Content-Type: application/json

{
  "titulo": "Cien años de soledad",
  "genero": "Realismo mágico",
  "fechaPublicacion": "1967-06-05",
  "isbn": "978-0060883287",
  "autorIds": [1, 2]
}
```

#### Actualizar un libro

```
PUT /api/Libros/{id}
Content-Type: application/json

{
  "titulo": "Cien años de soledad (Edición revisada)",
  "genero": "Realismo mágico",
  "fechaPublicacion": "1967-06-05",
  "isbn": "978-0060883287",
  "autorIds": [1]
}
```

#### Eliminar un libro

```
DELETE /api/Libros/{id}
```

---

### Autores

#### Obtener todos los autores

```
GET /api/Autores
```

#### Obtener un autor por ID

```
GET /api/Autores/{id}
```

#### Crear un autor

```
POST /api/Autores
Content-Type: application/json

{
  "nombre": "Gabriel",
  "apellido": "García Márquez",
  "fechaNacimiento": "1927-03-06",
  "nacionalidad": "Colombiana"
}
```

#### Actualizar un autor

```
PUT /api/Autores/{id}
Content-Type: application/json

{
  "nombre": "Gabriel",
  "apellido": "García Márquez",
  "fechaNacimiento": "1927-03-06",
  "nacionalidad": "Colombiana"
}
```

#### Eliminar un autor

```
DELETE /api/Autores/{id}
```

---

### LibroAutores (Relación Libro-Autor)

#### Obtener todas las relaciones

```
GET /api/LibroAutores
```

#### Obtener una relación por ID

```
GET /api/LibroAutores/{id}
```

#### Crear una relación

```
POST /api/LibroAutores
Content-Type: application/json

{
  "libroId": 1,
  "autorId": 1
}
```

#### Actualizar una relación

```
PUT /api/LibroAutores/{id}
Content-Type: application/json

{
  "libroId": 1,
  "autorId": 2
}
```

#### Eliminar una relación

```
DELETE /api/LibroAutores/{id}
```
