import "./DocumentationForm.css";
import FileUploadIcon from '@mui/icons-material/FileUpload';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import { DataGrid } from '@mui/x-data-grid';
import Box from '@mui/material/Box';
import Backdrop from '@mui/material/Backdrop';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Typography from '@mui/material/Typography';
import { useEffect, useMemo, useState } from "react";
import TextField from '@mui/material/TextField';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { useParams } from "react-router-dom";
import CloseIcon from '@mui/icons-material/Close';
import { useUploadDocumentMutation, useLazyGetDocumentsByProjectQuery } from "../../features/documentsApi/documentsApiSlice";
import { useSnackbar } from 'notistack';

const DocumentationForm = () => {
    const [isOpen, setIsOpen] = useState(false);
    const { projectId } = useParams();
    const [documents, setDocuments] = useState([]);
    const [getDocumentsByProjectFetch, {isLoading: isDocumentsLoading}] = useLazyGetDocumentsByProjectQuery();

    useEffect(() => {
        const load = async () => {
            await loadData();
        }

        load();
    },[])

    const loadData = async () => {
        await getDocumentsByProjectFetch(projectId).unwrap().then(data => setDocuments(data)).catch(err => console.log(err));
    }

    const columns = [
        { field: 'id', headerName: 'ID', width: 90, hide: true },
        {
            field: 'documentName',
            headerName: 'Название документа',
            width: 200,
            editable: false,
        },
        {
            field: 'documentDescription',
            headerName: 'Описание документа',
            width: 300,
            editable: false,
        },
        {
            field: 'fileName',
            headerName: 'Название файла документа',
            width: 200,
            editable: false,
        },
        {
            field: 'fileUrl',
            headerName: 'Ссылка на файл',
            width: 100,
            editable: false,
            hide: true
        },
        {
            field: 'UploadDate',
            headerName: 'Дата загрузки',
            width: 150,
            editable: false,
        }
    ];

    const handleUploadClick = (e) => {
        setIsOpen(true);
    }

    const uploadPopup = useMemo(() => {
        return (
            <TransitionsModal isOpen={isOpen} setIsOpen={setIsOpen} projectId={projectId} />
        )
    }, [isOpen]);

    return (
        <div className="documentation-form">
            {uploadPopup}
            <div className="documentation-form__header">
                <p>Документация проекта</p>
            </div>
            <div className="documentation-form__form-actions form-actions">
                <div className="form-actions__buttons">
                    <Button size="small" sx={{ color: 'black' }} onClick={e => handleUploadClick(e)}><FileUploadIcon />Загрузить документ</Button>
                </div>
            </div>
            <Divider />
            <div className="documentation-form__documents-list documents-list">
                <Box sx={{ height: 371, width: '100%' }}>
                    <DataGrid
                        rows={documents}
                        columns={columns}
                        rowsPerPageOptions={[5]}
                        pageSize={5}
                        disableRowSelectionOnClick
                        loading={isDocumentsLoading}
                    />
                </Box>
            </div>
        </div>
    )
}

export default DocumentationForm;

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    boxShadow: 24,
    p: 2,
    borderRadius: '10px',
    minHeight: '350px'
};

function TransitionsModal(props) {
    const { isOpen, setIsOpen, projectId } = props;
    const [open, setOpen] = useState(false);
    const [uploadedFile, setUploadedFile] = useState();
    const handleClose = () => {
        setIsOpen(false);
        setOpen(false);
    };

    const { enqueueSnackbar } = useSnackbar();

    const [uploadDocumentFetch] = useUploadDocumentMutation();

    useEffect(() => {
        setOpen(isOpen);
    }, [isOpen])

    const validationSchema = yup.object({
        documentName: yup
            .string('Введите название проекта')
            .required('Поле обязательно для заполенения'),
        documentDescription: yup
            .string('Введите описание проекта')
            .required('Поле обязательно для заполенения')
    });

    const formik = useFormik({
        initialValues: {
            documentName: '',
            documentDescription: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            onFormSubmit(values);
        },
    });

    const onFormSubmit = async (values) => {
        const requestData = new FormData();

        requestData.append('documentName', values.documentName);
        requestData.append('documentDescription', values.documentDescription);
        requestData.append('projectId', Number(projectId));
        requestData.append('uploadedFile', uploadedFile);

        await uploadDocumentFetch(requestData).unwrap().then(() => handleSuccessUploading()).catch(err => handleErrorUploading(err));
    }

    const handleSuccessUploading = () => {
        enqueueSnackbar('Документ успешно загружен', {variant:'success'});
        handleClose();
    }

    const handleErrorUploading = (error) => {
        enqueueSnackbar('Ошибка при загрузке документа', {variant:'error'});
    }

    const handleFileBind = (e) => {
        if(e.target.files.length > 1){
            alert("Для загрузки можно выбрать только 1 файл");
        }
        const file = e.target.files[0];
        setUploadedFile(file);
    }

    const canUpload = useMemo(() => {
        return formik.values.documentName.length !== 0 && formik.values.documentDescription.length !== 0 && uploadedFile;
    },[formik.values, uploadedFile])

    return (
        <div>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={open}
                onClose={handleClose}
                closeAfterTransition
                slots={{ backdrop: Backdrop }}
                slotProps={{
                    backdrop: {
                        timeout: 500,
                    },
                }}
            >
                <Fade in={open}>
                    <Box sx={style}>
                        <Button sx={{ position: 'absolute', top: 0, right: 0 }} onClick={handleClose}>
                            <CloseIcon />
                        </Button>
                        <Typography id="transition-modal-title" variant="h6" component="h2" sx={{ textAlign: 'center' }}>
                            Загрузка документа
                        </Typography>
                        <form className="upload-document-form" onSubmit={formik.handleSubmit}>
                            <div className="upload-document-form__upload-form-body upload-form-body">
                                <div className="upload-form-body__input-element input-element">
                                    <div className="input-element__label">
                                        <p>Название документа</p>
                                    </div>
                                    <div className="input-element__input-box">
                                        <TextField
                                            fullWidth
                                            id="documentName"
                                            name="documentName"
                                            value={formik.values.documentName}
                                            onChange={formik.handleChange}
                                            error={formik.touched.documentName && Boolean(formik.errors.documentName)}
                                            helperText={formik.touched.documentName && formik.errors.documentName}
                                            sx={{ height: '100% !important', width: '100%' }}
                                            size="small"
                                        />
                                    </div>
                                </div>
                                <div className="upload-form-body__input-element input-element">
                                    <div className="input-element__label">
                                        <p>Описание документа</p>
                                    </div>
                                    <div className="input-element__input-box">
                                        <TextField
                                            fullWidth
                                            id="documentDescription"
                                            name="documentDescription"
                                            value={formik.values.documentDescription}
                                            onChange={formik.handleChange}
                                            multiline
                                            maxRows={6}
                                            error={formik.touched.documentDescription && Boolean(formik.errors.documentDescription)}
                                            helperText={formik.touched.documentDescription && formik.errors.documentDescription}
                                            sx={{ height: '100% !important', width: '100%' }}
                                            size="small"
                                        />
                                    </div>
                                </div>
                                <div className="upload-form-body__input-element input-element">
                                    <div className="input-element__label">
                                        <p>Файл</p>
                                    </div>
                                    <div className="input-element__input-box">
                                        <input type="file" name="iIfile" id="iFile" onChange={e =>handleFileBind(e)}/>
                                    </div>
                                </div>
                            </div>
                            <div className="upload-document-form__form-bottom">
                                <Button variant="contained" type="submit" disabled={!canUpload}>
                                    Загрузить документ
                                </Button>
                            </div>
                        </form>
                    </Box>
                </Fade>
            </Modal>
        </div>
    );
}