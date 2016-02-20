package jfx_horario.login;

import com.itextpdf.text.Document;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.pdf.PdfWriter;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import jfx_horario.profesor.ProfesorController;
import misClases.BddConnection;
import misClases.Constantes;
import org.omg.CORBA.Environment;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Date;
import java.util.ResourceBundle;

/**
 * Created by Cristina on 27/01/2016.
 */
public class LoginController implements Initializable {

    @FXML
    TextField txtUsuario;
    @FXML
    PasswordField txtContra;
    @FXML
    Button btnLogin;
    @FXML
    Label lblError;

    private double posX, posY;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initViews();
    }

    private void initViews() {
        btnLogin.setOnAction(new EventHandler<ActionEvent>() { // cuando se hace clic en el bot�n
            @Override
            public void handle(ActionEvent event) {
                if (textFielRellenos()) // si los campos est�n rellenos
                    tryToloadNextWindow(); // trato de lanzar la nueva ventada.
            }
        });

        // Evento que voy a usar para que se lance la nueva ventana si se presiona la tecla enter
        EventHandler<KeyEvent> evento = new EventHandler<KeyEvent>() {
            @Override
            public void handle(KeyEvent event) {
                if (event.getCode().equals(KeyCode.ENTER)) { // si se ha presionado la tecla enter
                    if (textFielRellenos()) // y los campos est�n rellenos
                        tryToloadNextWindow(); // lanzo la ventana
                } else
                    lblError.setVisible(false);
            }
        };
        txtContra.setOnKeyPressed(evento);
        txtUsuario.setOnKeyPressed(evento);
        btnLogin.setOnKeyPressed(evento);
    }

    public boolean textFielRellenos() {
        boolean r = false;
        // si el campo de usuario no est� vac�o, la contrase�a tampoco est� vac�a y el se han introducido como m�ximo 3 caracteres en el campo de usuario
        if (!txtUsuario.getText().isEmpty() && !txtContra.getText().isEmpty() && txtUsuario.getText().length() <= 3) {
            r = true; // retorno true
        } else // en caso contrario
            lblError.setVisible(true); // muestro el mensaje de error
        return r;
    }

    public void tryToloadNextWindow() { // trato de lanzar la nueva ventana
        int tipoUser;
        Parent root = null;
        Stage stage;
        boolean logueadoConExito = false;
        String tituloWindow = "";

        tipoUser = consultarBDD(txtUsuario.getText(), txtContra.getText()); // consulto qu� tipo de usuario es el introducido
        try {
            if (tipoUser == Constantes.TIPO_PROFE) { // si se ha tratado de loguear un profesor
                FXMLLoader loader = new FXMLLoader(getClass().getResource(("../profesor/profesor.fxml"))); // al loader le indico que tiene que inflar las especificaciones xml del profesor
                loader.setController(new ProfesorController(txtUsuario.getText())); // asigno al loader el controlador del profesor, pasandole como argumento el codigo del profesor
                root = loader.load(); // inflo las especificaciones
                tituloWindow = "Horario Profesor";
                logueadoConExito = true;
            } else if (tipoUser == Constantes.TIPO_JEFATURA) { // si por el contrario, se ha tratado de loguear jefatura
                root = FXMLLoader.load(getClass().getResource("../jefatura/jefatura.fxml")); // inflo sus especificaciones
                tituloWindow = "Jefatura";
                logueadoConExito = true;
            } else {
                lblError.setVisible(true);
            }

            if (logueadoConExito) { // lanzo la nueva ventana
                stage = new Stage();
                stage.setTitle(tituloWindow);
                stage.setScene(new Scene(root));
                stage.setResizable(false);
                //stage.initStyle(StageStyle.UNDECORATED);
                configDragDropWindow(root, stage); // para que se pueda arrastrar la ventana
                stage.show();
                ((Stage)btnLogin.getScene().getWindow()).close(); // cierro la ventana de loguin
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private int consultarBDD(String idUsuario, String contra) {
        int r = -1;
        Connection conexion = BddConnection.newConexionMySQL("horario");
        PreparedStatement sentencia;
        ResultSet result;
        String select = "select tipo from profesor where codProf = ? and contra = ?;"; // obtengo el tipo del c�digo de profesor introducido por el usuario

        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, idUsuario);
            sentencia.setString(2, contra);
            result = sentencia.executeQuery();
            if (result.next())
                r = result.getByte(1);
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return r;
    }

    private void configDragDropWindow(Parent root, Stage stage){
        root.setOnMousePressed(event -> {
            posX = event.getX();
            posY =  event.getY();
        });

        root.setOnMouseDragged(event -> {
            stage.setX(event.getScreenX() - posX);
            stage.setY(event.getScreenY() - posY);
        });
    }
}
