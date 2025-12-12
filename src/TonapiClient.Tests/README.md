# TonapiClient Tests

Интеграционные тесты для TonapiClient.

## Настройка

1. Скопируйте `appsettings.example.json` в `appsettings.json`:
   ```bash
   cp appsettings.example.json appsettings.json
   ```

2. Откройте `appsettings.json` и замените `YOUR_API_KEY_HERE` на ваш API ключ:
   - Получить ключ можно на [tonapi.io](https://tonapi.io)
   - Для тестов рекомендуется использовать testnet API

3. Запустите тесты:
   ```bash
   dotnet test
   ```

## Примечания

- Файл `appsettings.json` включен в `.gitignore` и не должен коммититься
- Тесты выполняют реальные запросы к TON API testnet
- При запуске всех тестов подряд возможны ошибки rate limit - это нормально
- Для production использования получите API ключ с более высоким лимитом

