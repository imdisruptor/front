﻿class Index extends React.Component { 
    constructor(props) {
        super(props);
        this.state = { accessToken: "", messagesId: [], catalogsId: [] };
    }

    updateData = (value) => {
        this.setState({ accessToken: value });
    }

    addMessageId = (value) => {
        this.state.messagesId.push(value);
    }

    addCatalogId = (value) => {
        this.state.catalogsId.push(value);
    }

    render() {
        return (
            <div>
                <Auth update={this.updateData} />
                <CreateCatalogs addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <ChangeCatalogs addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <CreateMessages addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <DeleteMessages addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <ReadMessages addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <ReadCatalogs addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
                <DeleteCatalogs addCatalog={this.addCatalogId} addMessage={this.addMessageId} accessToken={this.state.accessToken} message={this.state.messagesId} catalog={this.state.catalogsId} />
            </div>);
    }
}

class Auth extends React.Component {

    constructor(props) {
        super(props);
        this.state = { status: "Вы не авторизованы", mail: "ministoir@gmail.com", pass: "P_assword1", name: "", accessToken: "" };
        var token="";
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeMail = this.onChangeMail.bind(this);
        this.onChangePass = this.onChangePass.bind(this);
        this.auth = this.auth.bind(this);
        this.autho = this.autho.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeMail(e) {
        var val = e.target.value;
        this.setState({ mail: val });
    }

    onChangePass(e) {
        var val = e.target.value;
        this.setState({ pass: val });
    }

    auth(e) {
        this.autho(e);
        this.props.update(token);
        if (token != "") this.setState({ status: "Вы авторизованы" });
    }

    autho(e) {
        var json = JSON.stringify({ email: this.state.mail, password: this.state.pass }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/token",false);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                var temp = JSON.parse(request.responseText).accessToken;
                token = temp;
            }
        }
        request.send(json);
    }
    
    render() {
        return (
            <div>
                <h2>Авторизация {this.state.status}</h2>
                <input type="text"
                    value={this.state.mail}
                    onChange={this.onChangeMail} />
                <input type="text"
                    value={this.state.pass}
                    onChange={this.onChangePass} />
                <button onClick={this.auth}> Enter </button>
            </div>
        );
    }
}
/*
                 <input id="token" type="text"
                      value={this.state.name} />
*/


class CreateCatalogs extends React.Component {

    constructor(props) {
        super(props);
        this.state = { title: "TestTitle", pid: "", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeTitle = this.onChangeTitle.bind(this);
        this.onChangePID = this.onChangePID.bind(this);
        this.createCatalog = this.createCatalog.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeTitle(e) {
        var val = e.target.value;
        this.setState({ title: val });
    }

    onChangePID(e) {
        var val = e.target.value;
        this.setState({ pid: val });
    }

    createCatalog(e) {
        var json = JSON.stringify({ title: this.state.title, parentscatalogid: this.state.pid }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/catalogs");
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 201) {
                var temp = JSON.parse(request.responseText).id;
                document.getElementById('id0').value = temp;
                document.getElementById('id').value = temp;
                document.getElementById('id4').value = temp;
                document.getElementById('id10').value = temp;
                document.getElementById('id12').value = temp;
            }
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <h2>Создание каталога</h2>
                <input type="text"
                    value={this.state.title}
                    onChange={this.onChangeTitle} />
                <input type="text"
                    placeholder="catalog's id"
                    value={this.state.pid}
                    onChange={this.onChangePID} />
                <button onClick={this.createCatalog}> Enter </button>
                <input id="id" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}
/*
                <input id="id" type="text"
                    value={this.state.name} />
*/


class ChangeCatalogs extends React.Component {

    constructor(props) {
        super(props);
        this.state = { text: "ministoir@gmail.com", id: "", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.onChangeText = this.onChangeText.bind(this);
        this.changeCatalog = this.changeCatalog.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState(prevState => ({ id: val }));
    }

    onChangeText(e) {
        var val = e.target.value;
        this.setState(prevState => ({ text: val }));
    }

    changeCatalog(e) {
        var json = JSON.stringify({ title: this.state.text}, null, ' ');
        var request = new XMLHttpRequest();
        request.open("PUT", "http://localhost:57059/api/v1/catalogs/" + this.state.id);
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                var temp = JSON.parse(request.responseText).title;
                document.getElementById('id2').value = temp;
            }
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.text}
                    onChange={this.onChangeText} />
                <input type="text"
                    id="id0"
                    value={this.state.id}
                    onChange={this.onChangeID} />
                <button onClick={this.changeCatalog}> Enter </button>
                <input id="id2" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

class CreateMessages extends React.Component {
    constructor(props) {
        super(props);
        this.state = { catalogid: "catalogId", subject: "Subject", text: "Text", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.onChangeText = this.onChangeText.bind(this);
        this.onChangeSubject = this.onChangeSubject.bind(this);
        this.createMessage = this.createMessage.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ catalogid: val });
    }

    onChangeText(e) {
        var val = e.target.value;
        this.setState({ text: val });
    }

    onChangeSubject(e) {
        var val = e.target.value;
        this.setState({ subject: val });
    }

    createMessage(e) {
        var json = JSON.stringify({ subject: this.state.subject, catalogid: this.state.catalogid, title: this.state.text }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/messages/");
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 201) {
                var temp = JSON.parse(request.responseText).id;
                alert(temp);
                document.getElementById('id3').value = temp;

                document.getElementById('id6').value = temp;
                document.getElementById('id8').value = temp;
                id2 = temp;
            }
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.text}
                    onChange={this.onChangeText} />
                <input type="text"
                    id="id4"
                    value={this.state.catalogid}
                    onChange={this.onChangeID} />
                <input type="text"
                    value={this.state.subject}
                    onChange={this.onChangeSubject} />
                <button onClick={this.createMessage}> Enter </button>
                <input id="id3" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

class DeleteMessages extends React.Component {
    constructor(props) {
        super(props);
        this.state = { messageid:"", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.createMessage = this.createMessage.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ messageid: val });
    }

    createMessage(e) {
        var request = new XMLHttpRequest();
        request.open("DELETE", "http://localhost:57059/api/v1/messages/" + this.state.messageid);
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                document.getElementById('id5').value = 'Deleted';
            }
        }
        request.send();
    }

    render() {
        return (
            <div>
                <input type="text"
                    id="id6"
                    value={this.state.messageid}
                    onChange={this.onChangeID} />
                <button onClick={this.createMessage}> Enter </button>
                <input id="id5" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

class ReadMessages extends React.Component {
    constructor(props) {
        super(props);
        this.state = { messageid: "", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.readMessage = this.readMessage.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ messageid: val });
    }

    readMessage(e) {
        var request = new XMLHttpRequest();
        request.open("GET", "http://localhost:57059/api/v1/messages/" + this.state.messageid);
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                var temp = JSON.parse(request.responseText).subject;
                document.getElementById('id7').value = temp;
            }
        }
        request.send();
    }

    render() {
        return (
            <div>
                <input type="text"
                    id="id8"
                    value={this.state.messageid}
                    onChange={this.onChangeID} />
                <button onClick={this.readMessage}> Enter </button>
                <input id="id7" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

class ReadCatalogs extends React.Component {
    constructor(props) {
        super(props);
        this.state = { catalogid: "", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.readCatalog = this.readCatalog.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ catalogid: val });
    }

    readCatalog(e) {
        var request = new XMLHttpRequest();
        request.open("GET", "http://localhost:57059/api/v1/catalogs/" + this.state.catalogid);
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                var temp = JSON.parse(request.responseText).title;
                document.getElementById('id9').value = temp;
            } else alert(request.status);
        }
        request.send();
    }

    render() {
        return (
            <div>
                <input type="text"
                    id="id10"
                    value={this.state.catalogid}
                    onChange={this.onChangeID} />
                <button onClick={this.readCatalog}> Enter </button>
                <input id="id9" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

class DeleteCatalogs extends React.Component {
    constructor(props) {
        super(props);
        this.state = { catalogid: "", name: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.deleteCatalog = this.deleteCatalog.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ catalogid: val });
    }

    deleteCatalog(e) {
        var request = new XMLHttpRequest();
        request.open("DELETE", "http://localhost:57059/api/v1/catalogs/" + this.state.catalogid);
        request.setRequestHeader('Authorization', 'Bearer ' + this.props.accessToken);
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                document.getElementById('id11').value = 'Catalog was deleted';
            }
        }
        request.send();
    }

    render() {
        return (
            <div>
                <input type="text"
                    id="id12"
                    value={this.state.catalogid}
                    onChange={this.onChangeID} />
                <button onClick={this.deleteCatalog}> Enter </button>
                <input id="id11" type="text"
                    value={this.state.name} />
            </div>
        );
    }
}

ReactDOM.render(
    <Index />,
    document.getElementById("content")
);
