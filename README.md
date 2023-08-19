
## HTTP Durum Kodları

- **200 OK:** İstek başarılı bir şekilde gerçekleşti ve sunucu istenen veriyi geri döndürdü.

- **201 Created:** İstek sonucunda yeni bir kaynak başarıyla oluşturuldu.

- **202 Accepted:** İstek işlenmeye kabul edildi, ancak işleme henüz tamamlanmış olmayabilir.

- **204 No Content:** Sunucu isteği başarıyla işledi, ancak cevapta gönderilecek veri yok. Genellikle silme gibi cevap gövdesi gerektirmeyen istekler için kullanılır.

- **400 Bad Request:** Sunucu, isteği, hatalı sözdizimi veya geçersiz istek parametreleri gibi istemci hatası nedeniyle anlayamadı.

- **404 Not Found:** Sunucu, istenen kaynağı bulamadı.

- **409 Conflict:** İstek, hedef kaynağın mevcut durumuyla çakışma nedeniyle tamamlanamadı. Bu tür bir çatışma, istek ve kaynağın mevcut durumu arasında bir çatışma olduğunda ortaya çıkabilir.

## HTTP Yöntemleri

HTTP (Hypertext Transfer Protocol), web tarayıcıları ve sunucular arasında iletişim kurmak için kullanılan protokoldür. Aşağıda yaygın olarak kullanılan HTTP yöntemleri kısaca açıklanmıştır:

**GET** yöntemi, sunucudan belirtilen kaynağın verilerini almak için kullanılır. İstek gövdesi içermez ve genellikle okuma işlemleri için kullanılır.

**POST** yöntemi, sunucuya yeni veri göndermek veya kaynak oluşturmak için kullanılır. İstek gövdesi içerir ve genellikle veri kaydetme veya oluşturma işlemleri için kullanılır.

**PUT** yöntemi, belirtilen kaynağı sunucuya gönderilen veri ile değiştirmek veya güncellemek için kullanılır. Eğer kaynak varsa üzerine yazar, yoksa oluşturur.

**DELETE** yöntemi, belirtilen kaynağı sunucudan silmek için kullanılır. Genellikle veri silme işlemleri için kullanılır.

**PATCH** yöntemi, belirtilen kaynağın sadece belirli kısımlarını güncellemek için kullanılır. Bu yöntemle kaynağın tamamını değiştirmek yerine belirli özellikleri güncellemek mümkündür.

Bu yöntemler, HTTP protokolünün temel işlevlerini temsil eder. API tasarımında ve kullanımında bu yöntemleri doğru bir şekilde kullanarak istenen işlemleri gerçekleştirebilirsiniz.

