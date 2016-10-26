﻿##Идея алгоритма:
Для того, чтобы поместить следующий прямоугольник, выполняем следующие действия:
* Рассматриваем все точки на карте, являющиеся вершинами некоторого прямоугольника.
* Для каждой такой точки - пробуем добавить новый прямоугольник с вершиной в этой точке.
* Для этого просто за линию пробегаемся по всем прямоугольникам и проверяем на пересечение с текущим.
* Из всех прямоугольников, которые удалось разместить таким образом выбираем ближайший к центру и размещаем его.

##Сложность алгоритма:
* Требуется расположить ![n](http://latex.codecogs.com/gif.latex?n) прямоугольников.
* Для каждого выполним ![O(n^2)](http://latex.codecogs.com/gif.latex?O%28n%5E2%29) операций, так как рассмотрим ![O(n)](http://latex.codecogs.com/gif.latex?O%28n%29) точек - вершин уже расположенных прямоугольников, и для каждой такой вершины попробуем поставить прямоугольник и проверить за ![O(n)](http://latex.codecogs.com/gif.latex?O%28n%29), что он не пересекается с остальными.

Итоговая оценка - ![O(n^3)](http://latex.codecogs.com/gif.latex?O%28n%5E3%29)

##Примеры сгенерированных изображений:

![Одинаковые прямоугольники](VisualizationExamples/similar.png)

![Случайные прямоугольники](VisualizationExamples/random.png)

![Случайные длинные прямоугольники](VisualizationExamples/randomLong.png)

![Большие прямоугольники](VisualizationExamples/Big.png)

