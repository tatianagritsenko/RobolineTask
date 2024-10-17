# Задание для Роболайн

## Инструкция по запуску

1. Клонировать репозиторий через командную строку:<br>
git clone https://github.com/tatianagritsenko/RobolineTask
2. Открыть файл RobolineTestTask.sln в Visual Studio с установленным .NET 8
3. Запустить приложение

После запуска приложения открывается Swagger UI, который позволяет протестировать API

В приложении есть 2 контроллера: 
- ProductCategoryController
- ProductController

Каждый контроллер выполняет CRUD-операции c соответствующими сущностями:
- ProductCategory
- Product

Валидация данных реализована с помощью атрибутов DataAnnotations

База данных Roboline.db расположена в папке Database.<br>
Строка подключения: "Data Source=./Database/Roboline.db"

**Протокол ProductCategoryController:**<br>
GET "api/ProductCategory/categories" - возвращает список категорий продуктов

GET "api/ProductCategory/categories/{id}" - возвращает категорию. Принимает id категории

POST "api/ProductCategory/categories" - добавляет новую категорию. Принимает объект ProductCategory в формате JSON, возвращает созданную категорию

PUT "api/ProductCategory/categories/{id}" - обновляет информацию о категории. Принимает id категории и объект ProductCategory в формате JSON, возвращает обновлённую категорию

DELETE "api/ProductCategory/categories/{id}" - удаляет категорию. Принимает id категории


**Протокол ProductController:**<br>
GET "api/Product/products" - возвращает список продуктов

GET "api/Product/products/{id}" - возвращает информацию о продукте. Принимает id продукта

POST "api/Product/products" - добавляет новую запись о продукте. Принимает объект Product в формате JSON, возвращает новый продукт

PUT "api/Product/products/{id}" - обновляет информацию о продукте. Принимает id продукта и объект Product в формате JSON, возвращает обновлённую запись о продукте

DELETE "api/Product/products/{id}" - удаляет продукт. Принимает id продукта


Комментарий:

Знаю, что в контроллерах ProductController и ProductCategoryController дублируется код. Пыталась это исправить, создав базовый контроллер, который принимает контекст базы данных и проводит CRUD-операции с соответствующими сущностями Product и ProductCategory, но идея не сработала, потому что не получилось преобразовать эти сущности к одному типу.







