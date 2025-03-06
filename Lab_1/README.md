# Документация транспортной сети

## Общее описание
Программная система моделирует транспортную сеть, включая управление транспортом, дорогами, светофорами и инфраструктурой.  
**Основной функционал**:
- Планирование и выполнение маршрутов транспорта.
- Управление светофорами и перекрестками.
- Проверка безопасности дорог и обслуживание инфраструктуры.
- Автоматическая симуляция жизненного цикла системы.

---

## Ключевые компоненты

### 1. Интерфейсы
- **`IVehicle`**  
  ```csharp
  internal interface IVehicle {
      int VehicleId { get; }
      string VehicleType { get; }
      string Position { get; set; }
      void Move();
  }
  ```
  Базовый контракт для транспорта. Реализуется классом `Transport`.

- **`IRoute`**  
  ```csharp
  internal interface IRoute {
      List<string> Route { get; set; }
      void PlanRoute(string endOfRoute);
  }
  ```
  Управление маршрутами (планирование, перемещение).

- **`IRoads`** и **`ICrossroads`**  
  Описывают свойства дорог (название, длина, загруженность) и перекрестков (светофоры, привязанные дороги).

---

### 2. Основные классы
#### **Транспорт (`Transport`)**  
```csharp
public class Transport : IVehicle, IRoute {
    public void PlanRoute(string endPosition)
    public void Move()
    public void ServiceVehicle()
}
```
- **Методы**:
  - `PlanRoute()`: Создает маршрут из текущей позиции в указанную точку.
  - `Move()`: Перемещает транспорт, обновляя позицию. Выбрасывает `RouteNotPlannedException`, если маршрут не задан.
  - `ServiceVehicle()`: Логирует факт обслуживания транспорта.

#### **Дорога (`Road`)**  
```csharp
public class Road : IRoads {
    public double Congestion { get; set; }
}
```
- **Свойства**: Название, длина, список машин, загруженность.

#### **Светофор (`TrafficLight`)**  
```csharp
public class TrafficLight {
    public enum TrafficLightState { Red, Yellow, Green }
    public void ChangeState()
}
```

#### **Дополнительные объекты**:
- `Crossroad`: Перекресток со светофором и списком дорог.
- `BikePath`: Велодорожка (длина, выделенность).
- `Sidewalk`: Пешеходная зона (ширина, материал).

---

### 3. Вспомогательные модули
#### **Управление светофорами (`TrafficManager`)**  
```csharp
public static void ManageTraffic(List<TrafficLight> trafficLights) {
    foreach (var light in trafficLights) light.ChangeState();
}
```

#### **Проверка безопасности (`SafetyAssurance`)**  
- Проверяет:
  - Наличие маршрутов у транспорта.
  - Загруженность дорог (если `Congestion > Distance / 100`).

#### **Обслуживание инфраструктуры (`InfrastructureMaintenance`)**  
```csharp
public static void PerformMaintenance(List<Road> roads) {
    road.Congestion = road.Cars.Count * 0.5;
}
```

---

### 4. Исключения
| Класс                          | Условие                          | Сообщение                          |
|--------------------------------|----------------------------------|------------------------------------|
| `RouteNotPlannedException`     | Вызов `Move()` без маршрута      | "Маршрут не запланирован!"         |
| `InvalidRouteException`        | Некорректная конечная точка      | Зависит от контекста (например, "Конечная точка совпадает с текущей") |

---

## Пример использования
```csharp
Transport bus = new Transport(2, "Автобус", "Остановка 1");

bus.PlanRoute("Остановка 2");

bus.Move();
```

---

## Симуляция жизненного цикла
**Запускается через `RunSimulation` в `Program.cs`**:
1. На каждом шаге:
   - Случайно создается новый транспорт (30% вероятность).
   - Транспорт планирует маршрут или удаляется из системы (50% вероятность).
   - Все транспортные средства перемещаются.
   - Светофоры переключаются.
2. **Пример логики**:
   ```csharp
   if (random.NextDouble() < 0.3) {
       vehicles.Add(new Transport(id, "Такси", "Точка X"));
   }
   foreach (var vehicle in vehicles) {
       vehicle.Move();
   }
   ```

---

```