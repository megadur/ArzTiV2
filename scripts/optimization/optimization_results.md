Before
````
Limit  (cost=1.04..985.49 rows=1000 width=41) (actual time=0.081..17.438 rows=1000 loops=1)
  ->  Merge Join  (cost=1.04..99440.41 rows=101010 width=41) (actual time=0.080..17.372 rows=1000 loops=1)
        Merge Cond: (e.id_senderezepte_emuster16 = d.id_senderezepte_emuster16)
        ->  Index Only Scan Backward using er_senderezepte_emuster16_pkey on er_senderezepte_emuster16 e  (cost=0.42..5635.98 rows=216504 width=4) (actual time=0.047..0.379 rows=1000 loops=1)
              Heap Fetches: 0
        ->  Index Scan Backward using idx_er_sendrez_em16_daten_id on er_senderezepte_emuster16_daten d  (cost=0.42..92000.58 rows=101010 width=41) (actual time=0.013..16.648 rows=1000 loops=1)
              Filter: (NOT transfer_arz)
Planning Time: 168.886 ms
Execution Time: 22.365 ms
````````

after

````
Limit  (cost=1.76..992.88 rows=1000 width=41) (actual time=0.062..1.336 rows=1000 loops=1)
  ->  Merge Join  (cost=1.76..100232.24 rows=101129 width=41) (actual time=0.061..1.267 rows=1000 loops=1)
        Merge Cond: (d.id_senderezepte_emuster16 = e.id_senderezepte_emuster16)
        ->  Index Scan Backward using idx_er_sendrez_em16_daten_id on er_senderezepte_emuster16_daten d  (cost=0.42..92790.91 rows=101129 width=41) (actual time=0.029..0.578 rows=1000 loops=1)
              Filter: (NOT transfer_arz)
        ->  Index Only Scan using idx_emuster16_id_ordering on er_senderezepte_emuster16 e  (cost=0.42..5635.98 rows=216504 width=4) (actual time=0.030..0.505 rows=1000 loops=1)
              Heap Fetches: 0
Planning Time: 6.028 ms
Execution Time: 1.405 ms

````
