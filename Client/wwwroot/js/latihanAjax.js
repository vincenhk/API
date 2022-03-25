//$.ajax({
//    url: "https://swapi.dev/api/people/",
//    success: function (result) {
//        console.log(result.results);
//        var text = "";
//        $.each(result.results, function (key, val) {
//            text += `<tr>
//            <td>${key + 1}</td>
//            <td>${val.name}</td>
//            <td>${val.height}</td>
//            <td>${val.mass}</td>
//            <td>${val.hair_color}</td>
//            <td>${val.skin_color}</td>
//        </tr>`;
//        })
//        console.log(text);
//        $('#tableStrwars').html(text);
//    }
//})



//const { data } = require("jquery");

//$.ajax({
//    url: "https://swapi.dev/api/people/"
//}).done((result) => {
//    console.log(result.results);
//    var text = "";
//    $.each(result.results, function (key, val) {
//        text += `<tr>
//            <td>${key + 1}</td>
//            <td>${val.name}</td>
//            <td>${val.height}</td>
//            <td>${val.mass}</td>
//            <td>${val.hair_color}</td>
//            <td>${val.skin_color}</td>
//        </tr>`;
//    })
//    console.log(text);
//    $('#tableStrwars').html(text);
//}).fail((err) => {
//    console.log(err);
//})

//fetch('https://pokeapi.co/api/v2/pokemon')
//    .then(
//        function (response) {
//            if (response.status != 200) {
//                console.log('Opss...' + response.status)
//                return
//            }

//            response.json().then(function (data) {
//                const pokemons = data.results
//                pokemons.forEach(pokemon => function(val, i) {
//                    document.getElementById('tablePoke').
//                        insertAdjacentHTML('beforeend',
//                            `<tr>
//                                <td>${pokemon.i + 1}</td>
//                                <td>${pokemon.val.name}</td>
//                                <td><button type="button" id="nameDetail" class="btn btn-primary" data-toggle="modal" data-target="#ModalPoke" onclick = "showDetail('${pokemon.val.name}')">Detail</button></td>
//                              </tr>`
//                    )
//                })
//            })
//        }
//)
//    .catch(function (err) {
//        console.log(err)
//    })

//$.ajax({
//    url: "https://swapi.dev/api/people/",
//    success: function (result) {
//        console.log(result.results);
//        var text = "";
//        $.each(result.results, function (key, val) {
//            text += `${val.name}`;
//        })
//        console.log(text);
//        $('#nameDetail').html(text);
//    }
//})

//function showBio(url) {
//    $.ajax({
//        url: `${svUrl}`,
//        success: function (pokemon) {
//            var text = "";
//            text += `<ul typeof = "disc">
//                        <li>Height\t: ${pokemon.height}</li>
//                        <li>Weight\t: ${pokemon.weight} </li>
//                        <li></li>
//                    </ul>`;
//            $("nav-bio").html(text);
//            console.log(svUrl);
//        }
//    })
//}

const colors = {
	fire: '#FDDFDF',
	grass: '#DEFDE0',
	electric: '#FCF7DE',
	water: '#DEF3FD',
	ground: '#f4e7da',
	rock: '#d5d5d4',
	fairy: '#fceaff',
	poison: '#98d7a5',
	bug: '#f8d5a3',
	dragon: '#97b3e6',
	psychic: '#eaeda1',
	flying: '#F5F5F5',
	fighting: '#E6E0D4',
	normal: '#F5F5F5'
};


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
   /* console.log(result.results);*/
    var text = "";
    //looping eachj
    $.each(result.results, (function (key, val){
        text += `<tr>
            <td>${key + 1}</td>
            <td>${val.name}</td>
            <td><button type="button" id="nameDetail" class="btn btn-primary" data-toggle="modal" data-target="#ModalPoke" onclick = "showDetail('${val.url}')">Detail</button></td>
        </tr>`;
    })
    )
/*    console.log(text);*/
    $('#tablePoke').html(text);
}).fail((err) => {
    console.log(err);
})

function showDetail(url) {
    fetch(url).then(function (response) {
        response.json().then(function (pokemon) {

            let text = document.getElementById('detailName').innerHTML = pokemon.name;
            document.getElementById("detailName").innerHTML = `DETAIL ${text.toUpperCase()}`;

            document.getElementById('pngPoke').innerHTML = '';

            var img = pokemon.sprites.other.dream_world.front_default;
            document.getElementById('pngPoke').insertAdjacentHTML('beforeend',
                `<img src='${img}' width="160" height="160">`
            );


            var text2 = "";
            text2 += `<div>
                      <div class="row"></div>
                    <div class="row sizeBody" >
                        <div class="col-lg border txtFont"><center id = "speciesHabitat"></center></div>
                        <div class="col-lg border txtFont"><center>Weight : </br> <i>${pokemon.weight / 10}g</i></center></div>
                        <div class="col-lg border txtFont"><center>Height : </br> <i>${pokemon.height / 10}m</i></center></div>
                    </div>
                      
                    <div class="row sizeBody">
                         <div class = "col border sizeBody" style : "border-radius: 15%;">
                         <label for="ability">Ability Type</label><h5><div class = "" name = "ability" id = "listAbility"></div></h5>
                      </div>
                    </div>

                    <div class="row sizeBody" >Evolution Pokemon
                    </div>

                    <div class="row border sizeBody" id = "show-evo">
                   </div>

                    <div class="row sizeBody" id = "show-evoName">
                   </div>

                    <div class="row sizeBody " >
                        <div class = "col " style ="font-size: 11px;" >Read Evolution from Left to Right <img src = 'https://i.pinimg.com/564x/56/5a/b3/565ab34a1af3c372c5813e145d8f382a.jpg' width ="25" height ="20"></div>
                   </div>
                 </div>`;
            document.getElementById('nav-bio').innerHTML = text2;

            var urlChain = "";
            var speciesUrl = pokemon.species.url;
            var temp = getSpeciesUrl(speciesUrl);
            var default_url = 'https://pokeapi.co/api/v2/pokemon';
           // console.log(temp);
            var getPoke = getPokeEvoChain(temp);
            showImg(default_url, getPoke);
            
           

            document.getElementById('nav-moves').innerHTML = listMoves(url);
            document.getElementById('nav-stats').innerHTML = listStats(url);
            document.getElementById('typeBadge').innerHTML = listTypes(url);
            pokeHabitat(speciesUrl);
            document.getElementById('listAbility').innerHTML = listAbility(url);

        })
    })
}


function showImg(url, temp) {
    let arr = temp;
    let aqq = [3];
    $.ajax({
        url: url,
        async: false
    }).done((img) => {
        var ar = img.results;
        var text = "";
        var imgUrl = "";
        
        $.each(img.results, (function (key, val) {

            if (val.name == temp[0]) {
                imgUrl = val.url;
                var tempUrl = realImg(imgUrl);
                arr[0] = tempUrl;
                aqq[0] = val.name;
            }
            else if (val.name == temp[1]) {
                imgUrl = val.url;
                var tempUrl = realImg(imgUrl);
                arr[1] = tempUrl;
                aqq[1] = val.name;
            }
            else if (val.name == temp[2]) {
                imgUrl = val.url;
                var tempUrl = realImg(imgUrl);
                arr[2] = tempUrl;
                aqq[2] = val.name;
            }
        })
        )

        for (var i = 0; i < arr.length; i++) {
            var source = arr[i]
            text += `<div class = "col shadow" ><img src='${source}' width="100" height="100"></div> `;
           // console.log(text);
        }
        $('#show-evo').html(text);

        var text_name = "";
        for (var i = 0; i < aqq.length; i++) {
            text_name += `<div class = "col sizeBody" >${aqq[i]}</div>`
            console.log(text_name);
        }

        $('#show-evoName').html(text_name);
    }).fail((err) => {
        console.log(err);
    })
}



function realImg(url) {
    var img = "";
    var txt = "";
    $.ajax({
        url: url,
        async: false
    }).done((pokemon => {
        img += pokemon.sprites.other.dream_world.front_default;

    })
    )
    return img;
}

function pokeHabitat(speciesUrl) {
    fetch(speciesUrl).then(response => response.json())
        .then(data => {
            var text = "";
            let species = document.getElementById('speciesHabitat');

            var habits = data.habitat.name;
            text += `Habitat : <br><i>${habits.toUpperCase().substring(habits.toUpperCase(), 1) + habits.slice(1)}</i>`;
            //console.log(text);


            species.innerHTML = text;
        }).catch(console.error);
}

function listMoves(url) {
    $.ajax({
        url: url
    }).done((pokemon) => {
        var text = "";
        $.each(pokemon.moves, (function (key, val) {
            text += `<li>${val.move.name}</li>`;
        })
        )
        $('#nav-moves').html(text);
    }).fail((err) => {
        console.log(err);
    })
}

function getSpeciesUrl(url) {
    var text = "";
    $.ajax({
        url: url,
        async: false
    }).done((data) => {

        text += data.evolution_chain.url;
        //console.log(text);
    }).fail((err) => {
        console.log(err);
    })
        return text;
}

function getPokeEvoChain(PokeEvoChainUrl) {
    var pokeEvoChain1 = "";
    var pokeEvoChain2 = "";
    var pokeEvoChain3 = "";
    const arrEvoChain = [pokeEvoChain1, pokeEvoChain2, pokeEvoChain3];

    $.ajax({
        url: PokeEvoChainUrl,
        async: false
    }).done((result) => {
        pokeEvoChain1 = result.chain.species.name;
        pokeEvoChain2 = result.chain.evolves_to[0].species.name;
        //CEK KALO ADA EVO 3 nya
        if (result.chain.evolves_to[0].evolves_to[0] != undefined) {
            pokeEvoChain3 = result.chain.evolves_to[0].evolves_to[0].species.name;
        }
        arrEvoChain[0] = pokeEvoChain1;
        arrEvoChain[1] = pokeEvoChain2;
        arrEvoChain[2] = pokeEvoChain3;
    }).fail((err) => {
        console.log(err);
    })
    //console.log(arrEvoChain);
    return arrEvoChain;
}

function listAbility(url) {
    $.ajax({
        url: url
    }).done((pokemon) => {
        var text = "";
        $.each(pokemon.abilities, (function (key, val) {
            text += `<span class="badge badge-info">${val.ability.name}</span> `;
            //console.log(text);
        })
        )
        $('#listAbility').html(text);
    }).fail((err) => {
        console.log(err);
    })
}

function listStats(url) {
    $.ajax({
        url: url
    }).done((pokemon) => {
        var text = "";
        var persen = 0;
        $.each(pokemon.stats, (function (key, val) {
        persen = val.base_stat;
            if (val.stat.name == "hp") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped" role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                          </div>`;
            }else if (val.stat.name == "attack") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped bg-danger" role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                          </div>`;
            }else if (val.stat.name == "defense") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped bg-warning" role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                          </div>`;
            } else if (val.stat.name == "special-attack") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped bg-info " role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                         </div>`;
            } else if (val.stat.name == "special-defense") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped bg-secondary" role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                         </div>`;
            } else if (val.stat.name == "speed") {
                text += `<div class="bar progress">
                              <div class="progress-bar progress-bar-striped bg-success" role="progressbar" style="width: ${persen}%;" aria-valuenow= "${persen}" aria-valuemin="0" aria-valuemax="100">${persen}%</div>
                         </div>`;
            }
        })
        )
        $('#nav-stats').html(text);
    }).fail((err) => {
        console.log(err);
    })
}

function listTypes(url) {
    $.ajax({
        url: url
    }).done((pokemon) => {
        var text = "";
        $.each(pokemon.types, (function (key, val) {
            if (val.type.name == 'grass') {
                text += `<span class="badge badge-success">${val.type.name}</span> `;
            }
            else if (val.type.name == 'water') {
                text += `<span class="badge badge-primary">${val.type.name}</span> `;
            }
            else if (val.type.name == 'fire') {
                text += `<span class="badge badge-danger">${val.type.name}</span> `;
            }
            else if (val.type.name == 'flying') {
                text += `<span class="badge badge-info">${val.type.name}</span> `;
            }
            else if (val.type.name == 'normal') {
                text += `<span class="badge badge-secondary">${val.type.name}</span> `;
            }
            else if (val.type.name == 'poison') {
                text += `<span class="badge badge-warning">${val.type.name}</span> `;
            }
            else {
                text += `<span class="badge badge-dark">${val.type.name}</span> `;
            }
        })
        )
        $('#typeBadge').html(text);
    }).fail((err) => {
        console.log(err);
    })
}







