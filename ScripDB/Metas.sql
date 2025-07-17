--Datos Rutinas
SELECT * FROM Ejercicios
INSERT INTO Ejercicios (Nombre, Categoria, ImagenURL)
VALUES 
('Sentadillas con Salto', 'Sentadillas', 'https://ik.imagekit.io/RunGym/RunGym/sentadillas.jpeg?updatedAt=1750101677760'),
('Flexi�n de Diamante', 'Flexiones', 'https://ik.imagekit.io/RunGym/RunGym/flexiones.jpeg?updatedAt=1750101678773'),
('Rotaciones', 'Hombro', 'https://ik.imagekit.io/RunGym/RunGym/flexiones.jpeg?updatedAt=1750101678773'),
('Walking Lunge (Zancadas Caminando)', 'Lunges', 'https://ik.imagekit.io/RunGym/RunGym/lunges.jpeg?updatedAt=1750101678437'),
('Biceps', 'Biceps', 'https://ik.imagekit.io/RunGym/RunGym/burpees.jpeg?updatedAt=1750101679129'),
('Espalda con superman', 'Espalda', 'https://ik.imagekit.io/RunGym/RunGym/climbers.jpeg?updatedAt=1750101679317'),
('Abdomen sin peso', 'Abdominales', 'https://ik.imagekit.io/RunGym/RunGym/abdominales.jpeg?updatedAt=1750101678073'),
('Extensiones de Tr�ceps', 'Triceps', 'https://ik.imagekit.io/RunGym/RunGym/skipping.jpeg?updatedAt=1750101677308')



SELECT * FROM DetallesEjercicio

INSERT INTO DetallesEjercicio (
    EjercicioId, Descripcion, Repeticiones, Descanso, Cuidados, URLVideo)
VALUES (
    1,
    'Realiza sentadillas con salto mientras sostienes pesas para trabajar las piernas, gl�teos y mejorar la explosividad.',
    '3 series de 12 repeticiones',
    '45 segundos entre series',
    'Mant�n la espalda recta y la rodilla alineada con el pie durante el salto para evitar lesiones.',
    'https://i.gifer.com/9yxa.gif'
),
(	
	2,
    'La flexi�n de diamante con salto es un ejercicio que fortalece los pectorales, triceps y hombros, al tiempo que mejora la agilidad y resistencia cardiovascular.',
    '4 series de 12 repeticiones',
    '45 segundos entre series',
    'Mant�n la espalda recta y la rodilla alineada con el pie durante el salto para evitar lesiones.',
    'https://i.gifer.com/Leoh.gif'
),
(	
	3,
    'Las rotaciones con peso son un ejercicio de core que mejora la estabilidad y fuerza de los m�sculos abdominales, oblicuos y espalda baja.',
    '4 series de 15 repeticiones',
    '30 segundos entre series',
    'Mant�n la espalda recta durante el movimiento y no realices el giro con demasiada velocidad para evitar lesiones en la columna.',
    'https://i.gifer.com/DHF3.gif'
),
(	
	4,
    'Realiza zancadas caminando para trabajar las piernas, gl�teos y mejorar el equilibrio.',
    '3 series de 12 repeticiones por pierna',
    '30-45 segundos entre series',
    'Mant�n la espalda recta, aseg�rate de que la rodilla no sobrepase los dedos del pie y realiza el movimiento con control.',
    'https://i.gifer.com/Q0Ey.gif'
),
(
	5,
    'Realiza curl de b�ceps con mancuernas para fortalecer los m�sculos de los brazos.',
    '3 series de 12 repeticiones por pierna',
    '60 segundos entre series',
    'Mant�n la espalda recta y no balancees las pesas para evitar lesiones.',
    'https://i.gifer.com/HoJi.gif'
),
(
	6,
    'Realiza acostado de manera que puedas estirar la espalda, gl�teos y abdomen. Este ejercicio ayuda a fortalecer la parte inferior de la espalda.',
    '3 series de 12 repeticiones',
    '45 segundos entre series',
    'Mant�n la espalda recta, acostado con las piernas completamente extendidas y los brazos arriba al estilo al�cuota.',
    'https://i.gifer.com/1yaV.gif'
),
(
	7,
    'Realiza abdominales tradicionales sin peso adicional para fortalecer los m�sculos centrales. Este ejercicio se enfoca en el trabajo de la zona abdominal.',
    '3 series de 20 repeticiones',
    '30 segundos entre series',
    'Evita forzar el cuello y mant�n el movimiento controlado para prevenir lesiones.',
    'https://i.gifer.com/XUeu.gif'
),
(
	8,
    'Realiza extensiones de tr�ceps con mancuerna para fortalecer y tonificar la parte posterior de los brazos, espec�ficamente los m�sculos tr�ceps.',
    '3 series de 10-12 repeticiones',
    '45-60 segundos entre series',
    'Mant�n los codos cerca de las orejas y realiza el movimiento de forma controlada para evitar lesiones.',
    'https://i.gifer.com/AQsw.gif'
)

--PASOS

SELECT * FROM PasosEjercicio

INSERT INTO PasosEjercicio(EjercicioId, NumeroPaso, DescripcionPaso)
VALUES
(1, 1, 'P�rate con los pies al ancho de los hombros y sujeta una pesa en cada mano.'),
(1, 2, 'Realiza una sentadilla profunda flexionando las rodillas y bajando las caderas.'),
(1, 3, 'Desde la posici�n baja, salta explosivamente hacia arriba mientras mantienes las pesas cerca del cuerpo.'),
(1, 4, 'Aterriza suavemente y repite el movimiento'),
(2, 1, 'Col�cate en posici�n de flexi�n, pero con las manos juntas formando un diamante bajo el pecho.'),
(2, 2, 'Haz una flexi�n normal, bajando el pecho hasta el suelo.'),
(2, 3, 'Al subir, realiza un salto explosivo con las manos y pies, aterrizando nuevamente en la posici�n inicial.'),
(2, 4, 'Repite el movimiento de forma controlada durante las repeticiones indicadas.'),
(3, 1, 'Si�ntate en el suelo con las piernas ligeramente dobladas, sujetando una pesa o bal�n medicinal con ambas manos.'),
(3, 2, 'Inclina ligeramente el torso hacia atr�s, manteniendo la espalda recta.'),
(3, 3, 'Gira el torso hacia un lado mientras mantienes las piernas estables.'),
(3, 4, 'Regresa al centro y gira hacia el otro lado. Repite el movimiento de forma controlada.'),
(4, 1, 'P�rate con los pies al ancho de los hombros y la espalda recta.'),
(4, 2, 'Da un paso largo hacia adelante con una pierna, bajando las caderas hasta que ambas rodillas est�n dobladas en un �ngulo de 90 grados.'),
(4, 3, 'Empuja con la pierna delantera para traer la pierna trasera hacia adelante y dar el siguiente paso.'),
(4, 4, 'Repite el movimiento alternando las piernas.'),
(5, 1, 'Si�ntate en una banca inclinada con una mancuerna en cada mano, con los brazos extendidos y los codos ligeramente doblados.'),
(5, 2, 'Sujeta las mancuernas con las palmas mirando hacia arriba.'),
(5, 3, 'Eleva las mancuernas hacia los hombros, flexionando los codos y concentr�ndote en la contracci�n del b�ceps.'),
(5, 4, 'Baja las mancuernas de forma controlada hasta la posici�n inicial, manteniendo siempre los codos fijos.'),
(6, 1, 'Acu�state en una colchoneta boca abajo, con las piernas completamente extendidas y los brazos extendidos hacia adelante.'),
(6, 2, 'Realiza una apertura de brazos y eleva las piernas, trabajando la parte superior de la espalda.'),
(6, 3, 'Desde la posici�n baja, salta explosivamente hacia arriba mientras mantienes las extremidades cerca del cuerpo.'),
(6, 4, 'Aterriza suavemente y repite el movimiento.'),
(7, 1, 'Recu�state en el suelo con las rodillas flexionadas y los pies apoyados en el suelo.'),
(7, 2, 'Cruza los brazos sobre el pecho o col�calos detr�s de la cabeza, sin jalar el cuello.'),
(7, 3, 'Levanta los hombros del suelo contrayendo el abdomen mientras exhalas.'),
(7, 4, 'Regresa lentamente a la posici�n inicial mientras inhalas.'),
(7, 5, 'Repite el movimiento con control.'),
(8, 1, 'Sujeta una mancuerna con ambas manos, extendiendo los brazos hacia arriba.'),
(8, 2, 'Baja la mancuerna detr�s de la cabeza, manteniendo los codos fijos y cerca de las orejas.'),
(8, 3, 'Extiende nuevamente los brazos hacia arriba, contrayendo el tr�ceps en el movimiento.'),
(8, 4, 'Repite el movimiento de forma lenta y controlada, concentr�ndote en la activaci�n del tr�ceps.');