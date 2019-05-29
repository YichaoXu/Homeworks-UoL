#### Question A
##### Question:
>Show that using the RDF(S) entailment rules it is not possible to derive the following triple
` :hasAuthors rdfs:domain :Publication .`

##### Solution:
According to the lecture slide, there are three kind of the RDF-entailment regimes, namely, Simple Entailment, RDF Entailment and RDFS Entailment.
###### 1. Simple Entailment:
Try to use the simple entailment deduction rules.
```
SE1:
u a x => u a _:n
SE2:
u a x => _:n a x
```
Because, the two rules will not change the predicate of a statement and the predicate `rdfs:domain` only occurs in the statement  `:hasAuthors rdfs:domain :conferenceArticle`. Therefore, the simple entailment will only derive the following triples and doesn't include the desired result.
```
_:n rdfs:domain :ConferenceArticle.
:hasAuthors rdfs:domain _:m.
_:n rdfs:domain _:m.
```
###### 2. RDF Entailment:
Because, the RDF Entailment is a set of interpretive function will only pertain the elements of RDF vocabulary. Then, It cannot derive the triple.
###### 3. RDFS Entailment:
In the original graph, only statement (11) describes relationship between `:Publication` and `:ConferenceArticle`. Additionally, only statement (14) describes the domain of property `:hasAuthors`.
```
:ConferenceArticle rdfs:subClassOf :Publication.
:hasAuthors rdfs:domain : ConferenceArticle.
```
Try to use RDFS inference rules on the two statement and the desired result cannot be derived.
###### 4. Conclusion:
It is impossible to derive the desired result through three RDF-entailment regimes. Therefore, using the RDF(S) entailment rules it is not possible to derive `:hasAuthors rdfs:domain :Publication .`
<br><br>
#### Question B
##### Question:
>Given G, verify if the following set of triples, S, is simple-entailed by G. Explain your answer by showing how S is contained in the new graph obtained applying simple entailment rules, if you believe that S is simple-entailed by G, otherwise justify your answer.
>```
>_:m1 :hasAuthors _:l1 .
>_:m1 rdf:type :W3CStandard .
>_:m1 rdfs:subClassOf :ConferenceArticle .
>```

##### Solution:
Try to use the simple entailment deduction rules.
```
SE1:
u a x => u a _:n
SE2:
u a x => _:n a x
```
Because, the two rules will not change the predicate of a statement.
1. The predicate `:hasAuthors` occurs in the statement  `SW:paper/147 :hasAuthors _:m`.
* empty node referenced by `_:m1` is introduced by rule se1 exactly for `SW:paper/147`
* empty node referenced by `_:l` is introduced by rule se2 exactly for `_:m`
2. The predicate `rdf:type` occurs in both of statements(1) `SW:paper/147 rdf:type  :ConferenceArticle` and statements(6) `w3c:TR/rdf11-mt rdf:type  :W3CStandard`.
* For statements(1), the simple entailment deduction rules cannot weaken object from `:ConferenceArticle` to `:W3CStandard`;
* For statement(6), the empty node referenced by `_:m1` has been introduced exactly for `SW:paper/147`

Therefore, it is impossible to derive the expected results from original graph.
<br><br>
#### Question C
##### Question:
>Given G, verify if the following set of triples, S, is RDFS-entailed by G. Explain your answer by showing how S is contained in the new graph obtained applying RDFS-entailment ruless, if you believe that S is RDFS-entailed by G, otherwise justify your answer.
>```
>_:m1 :has Authors _:l1 .
>_:m1 rdf:type _:m2 .
>_:m2 rdfs:subClassOf :Publication .
>```

##### Solution:
Try to use the simple entailment deduction rules.
1. Derive statement (2) `SW:paper/147 :hasAuthors _:m` to `_:m1 :hasAuthors _:l1`.
* empty node referenced by `_:l1` is introduced by rule se1 exactly for `_:m`.
* empty node referenced by `_:m1` is introduced by rule se2 exactly for `SW:paper/147`.
2. Derive statement (1) `SW:paper/147 rdf:type :ConferenceArticle` to `_:m1 rdf:type _:m2`
* empty node referenced by `_:m2` is introduced by rule se1 exactly for `:ConferenceArticle`.
3. Derive statement (11) `:ConferenceArticle rdfs:subClassOf ：Publication` to `_:m2 rdfs:subClassOf :Publication`

Because, these statements can be derived from original graph through the simple entailment which only rely on the graph transfer. Then, these statements can be derived through the RDFS entailment.
<br><br>
#### Question D
##### Question:
> Is it possible to RDFS-entail `SW:paper/147 :writtenBy _:m`. Explain your answer.
##### Solution:
In the original graph, only statement (2) describes the relationship between `SW:paper/147` and `_:m`. Additionally, only statement (13) describes the relationship of the properties `:hasAuthors` and `:writtenBy`.
```
SW:paper/147 :hasAuthors _:m.
:writtenBy rdfs:subClassOf : Publication.
```
Try to use RDFS inference rules (include rdfs7) on the two statement and the desired result cannot be derived.
