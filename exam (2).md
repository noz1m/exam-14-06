# 📦 Экзаменационный проект: Inventory Management System

## 📘 Описание проекта
Создайте простую систему управления складом (Inventory Management System), используя ASP.NET Core Web API. Проект должен содержать **5 таблиц** :

- Data Annotations
- Fluent API
- Migrations
- LINQ-запросы
- Паттерн Response
- Controllers
- Filtering
- Pagination

---

## 🧱 Таблицы

### 1. Products (Товары)
| Поле              | Тип данных    | Описание                     |
|-------------------|---------------|-------------------------------|
| Id                | int           | Первичный ключ                |
| Name              | string        | Название товара              |
| Price             | decimal       | Цена                         |
| QuantityInStock   | int           | Кол-во на складе             |
| CategoryId        | int           | Внешний ключ к Categories     |
| SupplierId        | int           | Внешний ключ к Suppliers      |

### 2. Categories (Категории)
| Поле    | Тип данных | Описание         |
|---------|------------|------------------|
| Id      | int        | Первичный ключ   |
| Name    | string     | Название категории |

### 3. Suppliers (Поставщики)
| Поле    | Тип данных | Описание        |
|---------|------------|-----------------|
| Id      | int        | Первичный ключ  |
| Name    | string     | Название        |
| Phone   | string     | Телефон         |

### 4. Sales (Продажи)
| Поле          | Тип данных | Описание                    |
|---------------|------------|-----------------------------|
| Id            | int        | Первичный ключ             |
| ProductId     | int        | Внешний ключ к Products     |
| QuantitySold  | int        | Количество продано         |
| SaleDate      | DateTime   | Дата продажи                |

### 5. StockAdjustments (Корректировки склада)
| Поле             | Тип данных | Описание                        |
|------------------|------------|---------------------------------|
| Id               | int        | Первичный ключ                  |
| ProductId        | int        | Внешний ключ к Products         |
| AdjustmentAmount | int        | На сколько изменился остаток   |
| Reason           | string     | Причина                         |
| AdjustmentDate   | DateTime   | Дата корректировки              |

---

## 📌 Требования к функционалу

### 1. Реализовать полный CRUD для всех таблиц:
- `POST /api/[table]` — создать
- `GET /api/[table]` — получить список
- `GET /api/[table]/{id}` — получить по Id
- `PUT /api/[table]/{id}` — обновить
- `DELETE /api/[table]/{id}` — удалить


---

## 🔟 Дополнительные API-запросы и их описание

### 1. `GET /api/categories/with-products`
**Описание:** Возвращает список всех категорий с вложенными товарами.
**Ответ:**
```json
[
  {
    "categoryId": 1,
    "categoryName": "Электроника",
    "products": [
      { "id": 1, "name": "Ноутбук", "price": 1200 },
      { "id": 2, "name": "Мышка", "price": 25 }
    ]
  }
]
```

### 2. `GET /api/products/low-stock`
**Описание:** Список товаров, у которых остаток меньше 5 штук.
**Ответ:**
```json
[
  { "id": 5, "name": "Клавиатура", "quantityInStock": 3 },
  { "id": 7, "name": "Принтер", "quantityInStock": 2 }
]
```

### 3. `GET /api/products/statistics`
**Описание:** Статистика по товарам: общее количество, средняя цена, общее количество продаж.
**Ответ:**
```json
{
  "totalProducts": 30,
  "averagePrice": 210.5,
  "totalSold": 450
}
```

### 4. `GET /api/sales/by-date?fromDate=2025-04-01&toDate=2025-04-15`
**Описание:** Продажи за указанный период.
**Ответ:**
```json
[
  {
    "saleId": 1,
    "productName": "Ноутбук",
    "quantitySold": 2,
    "saleDate": "2025-04-10T13:42:00"
  }
]
```

### 5. `GET /api/sales/top-products`
**Описание:** Топ-5 самых продаваемых товаров по количеству.
**Ответ:**
```json
[
  { "productName": "Ноутбук", "totalSold": 120 },
  { "productName": "Мышка", "totalSold": 90 }
]
```

### 6. `GET /api/reports/daily-revenue`
**Описание:** Доход по дням за последние 7 дней.
**Ответ:**
```json
[
  { "date": "2025-04-10", "revenue": 4000 },
  { "date": "2025-04-11", "revenue": 5300 }
]
```

### 7. `GET /api/stock-adjustments/history?productId=1`
**Описание:** История изменений остатков товара с ID 1.
**Ответ:**
```json
[
  { "adjustmentDate": "2025-04-09", "amount": -2, "reason": "Брак" },
  { "adjustmentDate": "2025-04-10", "amount": +10, "reason": "Поставка" }
]
```

### 8. `GET /api/products/details/{id}`
**Описание:** Полная информация о товаре, включая продажи и корректировки.
**Ответ:**
```json
{
  "id": 1,
  "name": "Ноутбук",
  "price": 1200,
  "quantityInStock": 8,
  "category": "Электроника",
  "supplier": "TechStore",
  "sales": [
    { "quantity": 2, "date": "2025-04-10" }
  ],
  "adjustments": [
    { "amount": +10, "date": "2025-04-05", "reason": "Поставка" }
  ]
}
```

### 9. `GET /api/suppliers/with-products`
**Описание:** Список поставщиков с перечнем поставляемых товаров.
**Ответ:**
```json
[
  {
    "supplierId": 1,
    "supplierName": "TechStore",
    "products": [ "Ноутбук", "Принтер" ]
  }
]
```

### 10. `GET /api/dashboard/statistics`
**Описание:** Общая статистика для дашборда: количество товаров, суммарный доход, количество продаж.
**Ответ:**
```json
{
  "totalProducts": 35,
  "totalRevenue": 25300,
  "totalSales": 87
}
```

---

## 📁 Дополнительные требования:
- Использовать Fluent API для связи между сущностями
- Реализовать пагинацию и фильтрацию на странице товаров
- Ответы от API должны быть в одном формате (Response pattern)
- Применить валидации через Data Annotations (например, `[Required]`, `[MaxLength]`, etc.)
- LINQ-запросы должны использоваться в сервисах

---

## 🎓 Цель экзамена
Показать умение:
- Проектировать таблицы и связи между ними
- Работать с миграциями
- Применять принципы чистой архитектуры
- Работать с запросами и валидациями

---



