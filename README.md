HepsiBuradaCaseStudy dizininde alttaki komut çalıştırılarak proje ayağa kaldırılır.

docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

Daha sonra http://localhost:8000/swagger/index.html adresine gidilerek api/v1/scenariofile endpointi çağrılır ve işlemler yapılır.

Proje içerisinde \HepsiBuradaCaseStudy\src\HepsiBuradaCaseStudy.Api\wwwroot\txt içerisinde scenario_file.txt isimli dosya okunarak komutlar çalıştırılır.
