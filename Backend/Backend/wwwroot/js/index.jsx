var accessToken;
var pass;
var mail;
var title;
var pid;
var id;
class Index extends React.Component { //index
    render() {
        return (
            <div>
                <Auth />
                <CreateCatalogs />
                <ChangeCatalogs />
            </div>);
    }
}

class Auth extends React.Component {

    constructor(props) {
        super(props);
        this.state = { mail: "ministoir@gmail.com", pass: "P_assword1", name: "" };
        mail = this.state.mail;
        pass = this.state.pass;
        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeMail = this.onChangeMail.bind(this);
        this.onChangePass = this.onChangePass.bind(this);
        this.autho = this.autho.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    onChangeMail(e) {
        var val = e.target.value;
        this.setState({ mail: val });
    }

    onChangePass(e) {
        var val = e.target.value;
        this.setState({ pass: val });
    }


    autho(e) {
        var json = JSON.stringify({ email: this.state.mail, password: this.state.pass }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/token");
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                var temp = JSON.parse(request.responseText);
                accessToken = temp.accessToken;
                
                alert(accessToken);
            }
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.mail}
                    onChange={this.onChangeMail} />
                <input type="text"
                    value={this.state.pass}
                    onChange={this.onChangePass} />
                <button onClick={this.autho}> Enter </button>
                <input type="text"
                    id="name1"
                    value={this.state.name}
                    onChange={this.onChange} />

            </div>
        );
    }
}

class CreateCatalogs extends React.Component {

    constructor(props) {
        super(props);
        this.state = { title: "TestTitle", pid: "11", name: "" };
        title = this.state.title;
        pid = this.state.pid;
        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeTitle = this.onChangeTitle.bind(this);
        this.onChangePID = this.onChangePID.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    onChangeTitle(e) {
        var val = e.target.value;
        this.setState({ title: val });
        title = val;
    }

    onChangePID(e) {
        var val = e.target.value;
        this.setState({ pid: val });
        pid = val;
    }

    createCatalog() {
        var json = JSON.stringify({ title: title, parentscatalogid: pid }, null, ' ');
        var request = new XMLHttpRequest();
        request.open("POST", "http://localhost:57059/api/v1/catalogs");
        request.setRequestHeader('Authorization', 'Bearer ' + accessToken);
        request.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 201) {
                id = JSON.parse(request.responseText).id;
                alert(id);
            } else alert(request.status);
        }
        request.send(json);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.title}
                    onChange={this.onChangeTitle} />
                <input type="text"
                    value={this.state.pid}
                    onChange={this.onChangePID} />
                <button onClick={this.createCatalog}> Enter </button>
                <input type="text"
                    value=""
                    onChange={this.onChange} />
            </div>
        );
    }
}

class ChangeCatalogs extends React.Component {

    constructor(props) {
        super(props);
        this.state = { text: "ministoir@gmail.com", id: "", name: "" };
        text = this.state.text;
        id = this.state.id;
        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onChangeID = this.onChangeID.bind(this);
        this.onChangeText = this.onChangeText.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    onChangeID(e) {
        var val = e.target.value;
        this.setState({ ID: val });
    }

    onChangeText(e) {
        var val = e.target.value;
        this.setState({ text: val });
    }


    changeCatalog() {
        alert(1);
        var request = new XMLHttpRequest();
        request.setRequestHeader('Authorization', 'Bearer ' + accessToken);
        function reqReadyStateChange() {
            if (request.readyState == 4) {
                var status = request.status;
                if (status == 200) {
                    var temp = request.responseText;
                    alert(temp);
                } else alert(request.status);
            }
        }
        // строка с параметрами для отправки
        alert(2);
        request.open("GET", "http://localhost:57059/api/v1/messages/" + id);
        request.onreadystatechange = reqReadyStateChange;
        request.send();
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.text}
                    onChange={this.onChangeText} />
                <input type="text"
                    value={this.state.id}
                    onChange={this.onChangeID} />
                <button onClick={this.changeCatalog}> Enter </button>
            </div>
        );
    }
}

ReactDOM.render(
    <Index />,
    document.getElementById("content")
);
