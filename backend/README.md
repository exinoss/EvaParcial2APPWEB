# Sistema de Gestión de Biblioteca - Backend API

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
