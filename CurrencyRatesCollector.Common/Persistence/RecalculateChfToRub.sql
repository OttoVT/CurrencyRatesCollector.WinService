	SELECT 
	chf_rate."Currency", 
	chf_rate."CreatedAt", 
	chf_rate."Value" as "CHF_Value", 
	rub_rate."Value" as "RUB_Value", 
	ROUND(rub_rate."Value" / chf_rate."Value", 4) as Value
	FROM currency_rates_collector.currency_rates as chf_rate
	INNER JOIN currency_rates_collector.currency_rates as rub_rate 
	ON chf_rate."CreatedAt" = rub_rate."CreatedAt" and rub_rate."Currency" = 'RUB'
	WHERE chf_rate."Currency" = 'CHF' and chf_rate."CreatedAt" > now() - interval '2 week'
	LIMIT 20 OFFSET 0;